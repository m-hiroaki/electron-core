'use strict';

const electron = require('electron');
const remote = electron.remote;

const { ipcRenderer } = require('electron');

ipcRenderer.on('asynchronous-message', (event, msg) => {
    document.getElementById('content').innerText = msg;
});