let currentSource;

let context;

let currentBuffer;

let currentAudioLink;

const currentApi = 'api/streaming/currentAudio';


window.onload = sensForButtons;


function sensForButtons() {
    'use strict';

    const buttonGet = document.getElementById('audio');
    buttonGet.addEventListener('click', prepareToStream);
}

function prepareToStream() {
    'use strict';

    let AudioContext = window.AudioContext || window.webkitAudioContext;

    context = new AudioContext();

    createAndPlaySilent();

    sync();
}

function saveAudio(URL) {
    'use strict';

    return fetch(URL)
        .then(response => response.arrayBuffer())
        .then(arrayBuffer =>
            context.decodeAudioData(
                arrayBuffer,
                audioBuffer => {
                    currentBuffer = audioBuffer;
                }, error => {
                    console.error(error);
                })
        )
}

function createAndPlaySilent() {
    'use strict';

    const pauseBuffer = context.createBuffer(2, context.sampleRate * 3, context.sampleRate);

    for (let channel = 0; channel < pauseBuffer.numberOfChannels; channel++) {

        let nowBuffering = pauseBuffer.getChannelData(channel);

        for (let i = 0; i < pauseBuffer.length; i++) {
            //nowBuffering[i] = Math.random() * 2 - 1;
            nowBuffering[i] = 0;
        }
    }

    const pauseSource = context.createBufferSource();

    pauseSource.buffer = pauseBuffer;

    pauseSource.connect(context.destination);

    pauseSource.loop = true;

    pauseSource.start();
}

function play() {
    'use strict';

    currentSource = context.createBufferSource();

    currentSource.buffer = currentBuffer;

    currentSource.connect(context.destination);

    currentSource.start();
}


function sync() {
    'use strict';

    console.log('Start sync');

    setInterval(getState, 1000);
}

function getState() {
    'use strict';

    let newLink;

    Promise.resolve(getLink(currentApi))
        .then(result => {
            newLink = result;
            return newLink;
        })
        .then(newLink => compare(newLink));
}

function compare(link) {
    console.log(link);

    if (link !== currentAudioLink) {

        if (currentAudioLink !== undefined) {
            currentSource.stop();
        }

        currentAudioLink = link;

        Promise.resolve(saveAudio(link))
            .then(play);
    }
}

function getLink(api) {
    'use strict';

    console.log('Make request for link');

    return fetch(api)
        .then(response => {
            console.log(response);

            // if (!response.ok) {
            //     throw new Error('HTTP error, status = ' + response.status);
            // }
            
            if(response.status == 406){
                currentSource.stop();
            }

            return response.text();
        });
}