let currentSource;

let context;

let currentAudioLink;

let performanceId;

let languagesId;

const performancesAPI = 'api/performance';

const languagesAPI = `api/performance/${performanceId}/languages/`;

const button = document.getElementById('connecting-button');

const performancePart = document.getElementById('performance-selection-part');

const languagePart = document.getElementById('language-selection-part');

const linkPart = document.getElementById('link-part');

const streamingPart = document.getElementById('streaming-part');


window.onload = init;


function init() {
    'use strict';

    getData(performancesAPI).then(response => {
        response.forEach(performance => {
            let button = document.createElement('button');
            button.setAttribute('id', performance.id);
            let title = document.createTextNode(performance.title);
            button.appendChild(title);
            performancePart.appendChild(button);
        })
    }).catch(error => 
        console.log(error)
    );

    performancePart.style.display = 'flex';
}

function goToStreamingPart() {
    'use strict';

    languagePart.style.display = 'none';

    streamingPart.style.display = 'flex';

    const connectButton = document.getElementById('connecting-button');
    connectButton.addEventListener('click', connectToStream);

}

function connectToStream() {
    'use strict';

    let AudioContext = window.AudioContext || window.webkitAudioContext;

    context = new AudioContext();

    createAndPlaySilent();

    connectToHub();

    changeButton();
}

function changeButton() {
    'use strict';

    button.style.backgroundColor = 'green';

    button.textContent = 'You are connected to stream';
}

function handleMessage(link) {
    'use strict';

    console.log('We get: ' + link);

    switch (link) {
        case 'Start':
            startStream();
            break;
        case 'End':
            endStream();
            break;
        case 'Resume':
            resumeStream();
            break;
        case 'Pause':
            pauseStream();
            break;
        case currentAudioLink:
            restartCurrentAudio();
            break;
        default:
            playNewAudio(link);
            break;
    }
}

function startStream() {
    'use strict';

    currentAudioLink = 'audio/Waiting.mp3';

    saveAndPlayAudio(currentAudioLink, true);
}

function endStream() {
    'use strict';

    currentSource.stop();

    displayLinks();
}

function resumeStream() {
    'use strict';

    saveAndPlayAudio(currentAudioLink);
}

function pauseStream() {
    'use strict';

    currentSource.stop();
}

function displayLinks() {
    'use strict';

    streamingPart.style.display = '';

    linkPart.style.display = 'flex';
}

function restartCurrentAudio() {
    'use strict';

    currentSource.stop();

    saveAndPlayAudio(currentAudioLink);
}

function playNewAudio(link) {
    link = 'audio/' + link + langId + '.mp3';

    if (currentAudioLink !== undefined) {
        currentSource.stop();
    }
    currentAudioLink = link;

    saveAndPlayAudio(currentAudioLink, false);
}

function saveAndPlayAudio(URL, audioLoop) {
    'use strict';

    console.log(URL);

    return fetch(URL)
        .then(response => response.arrayBuffer())
        .then(arrayBuffer =>
            context.decodeAudioData(
                arrayBuffer,
                audioBuffer => play(audioBuffer, audioLoop),
                error => console.error(error)
            )
        )
}

function connectToHub() {
    'use strict';

    let connection = new signalR.HubConnectionBuilder()
        .withUrl("/StreamHub")
        .build();

    connection.on("ReceiveMessage", function (message) {
        handleMessage(message);
    });

    connection.start().catch(function (err) {
        return console.error(err.toString());
    });
}

function createAndPlaySilent() {
    'use strict';

    const pauseBuffer = context.createBuffer(2, context.sampleRate * 3, context.sampleRate);

    for (let channel = 0; channel < pauseBuffer.numberOfChannels; channel++) {

        let nowBuffering = pauseBuffer.getChannelData(channel);

        for (let i = 0; i < pauseBuffer.length; i++) {
            // nowBuffering[i] = Math.random() * 2 - 1;
            nowBuffering[i] = 0;
        }
    }

    const pauseSource = context.createBufferSource();

    pauseSource.buffer = pauseBuffer;

    pauseSource.connect(context.destination);

    pauseSource.loop = true;

    pauseSource.start();
}

function play(currentBuffer, loopCondition) {
    'use strict';

    currentSource = context.createBufferSource();

    currentSource.buffer = currentBuffer;

    currentSource.connect(context.destination);

    currentSource.loop = loopCondition;

    currentSource.start();
}

function getData(api) {
    'use strict';
    return fetch(api)
        .then(response => {
            console.log(response);
            if (!response.ok) {
                throw new Error('HTTP error, status = ' + response.status);
            }
            return response.json();
        });
}




