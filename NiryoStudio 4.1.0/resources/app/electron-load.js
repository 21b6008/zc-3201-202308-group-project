// Require any node modules to be used here
// Need to do this until able to modify webpack config in Angular CLI
var fs = require('fs');
var electron = require('electron');
var path = require('path');
var url = require('url');
var client;

var dgram = require('dgram');
var buffer = require('buffer');
var os = require('os');
const {dialog} = require('electron').remote;
var appVersion = require('electron').remote.app.getVersion();

// Add all 3rd party node_modules included in the Electron app to be able to be used
if (!electron.remote.process.env.TESTING) {
    module.paths.push(path.resolve(electron.remote.app.getAppPath() + '/node_modules'));
}

/*
* Require external node modules here
*/
var compareVersions = require('compare-versions');
var deepcopy = require('deepcopy');
var lodash = require('lodash');
var fse = require('fs-extra');

// if LIVE_UPDATE env variable is true then use electron-connect
if (electron.remote.process.env.LIVE_UPDATE === "true") {
    client = require('electron-connect').client.create();
    client.on("reloadit", function() {
      electron.remote.getCurrentWindow().removeAllListeners();
      electron.remote.getCurrentWindow().loadURL(url.format({
            pathname: 'index.html',
            protocol: 'file:',
            slashes: true
        }));
    });
}