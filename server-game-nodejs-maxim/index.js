"use strict";

////////////////////////////////////////////////////////////////////////////////

const fs = require("fs");

function addToFile(fileName, content) {
    if (fs.existsSync(fileName)) {
        const oldContent = fs.readFileSync(fileName, "utf8");
        const newContent = oldContent + '\n' + content;
        fs.writeFileSync(fileName, newContent);
    } else {
        fs.writeFileSync(fileName, content);
    }
}

function getFileNames(folder) {
    const arr = fs.readdirSync(folder);
    arr.sort();
    return arr;
}

function deleteAllFiles(folder) {
    const arr = fs.readdirSync(folder);
    arr.forEach(nameString => {
        fs.unlinkSync(folder + nameString);
    });
}

////////////////////////////////////////////////////////////////////////////////

const express = require("express");

const app = express();
const port = 5015;
app.listen(port);
console.log("Server on port " + port);

////////////////////////////////////////////////////////////////////////////////

app.get("/game/api/save/story", function (request, response) {
    const userIdParam = request.query['userIdParam'];
    const storyMessageParam = request.query['storyMessageParam'];
    const fileName = './FILES/' + userIdParam + '.txt';
    const content = userIdParam + ' ' + storyMessageParam + ' ' + new Date().toISOString();
    addToFile(fileName, content);
    response.end(JSON.stringify({result: "OK", date: new Date().toISOString()}));
});

////////////////////////////////////////////////////////////////////////////////

app.get("/game/api/names/get/all", function (request, response) {
    const folder = './FILES/';
    const arr = getFileNames(folder);
    arr.sort();
    response.end(JSON.stringify({result: "OK", namesArr: arr, date: new Date().toISOString()}));
});

////////////////////////////////////////////////////////////////////////////////

app.get("/game/api/one/file/get", function (request, response) {
    const userIdParam = request.query['userIdParam'];
    const fileName = __dirname + '/FILES/' + userIdParam + '.txt';
    response.sendFile(fileName);
});

////////////////////////////////////////////////////////////////////////////////

app.get("/game/api/delete/all/files", function (request, response) {
    const folder = './FILES/';
    deleteAllFiles(folder);
    response.end(JSON.stringify({result: "OK", date: new Date().toISOString()}));
});

////////////////////////////////////////////////////////////////////////////////

app.get("/*", function (request, response) {
    const page = request.query['page'];
    let pathToPage = null;
    if('users' === page) pathToPage = __dirname + "/pages/" + 'usersListPage.html';
    if('menu' === page) pathToPage = __dirname + "/pages/" + 'menuActionsPage.html';
    if(pathToPage) {
        response.sendFile(pathToPage);
    } else {
        const htmlStringMessage = `<h1>Page not found</h1>`;
        response.end(htmlStringMessage);
    }
});
