'use strict';

var electron = require('electron');
var app = electron.app;
var BrowserWindow = electron.BrowserWindow;

var mainWindow = null;

require('http').createServer(function (request, response) {
  request.addListener('data', function (chunk) {
      const data = JSON.parse(chunk);
      mainWindow.webContents.send('asynchronous-message', data);
      response.end();
    }).resume();
}).listen(3000);

app.on('window-all-closed', function() {
  if(process.platform != 'darwin')
  app.quit();
});

app.on('ready', function() {
  mainWindow = new BrowserWindow(
    {
      width: 800, height: 600 ,
      webPreferences: {
        nodeIntegration: true
      }
    }
  );
  mainWindow.loadURL('file://' + __dirname + '/index.html');

  mainWindow.on('closed', function() {
    mainWindow = null;
  });
});