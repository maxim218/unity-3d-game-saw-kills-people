"use strict";

const WALL_COLOR = "blue";
const RED_PERSONS_COLOR = "red";
const HERO_COLOR = "gray";
const BASE_DEFAULT_WALLS_COLOR = "black";

const MAP_ELEMENT_SIZE = 40;

let typeSelected = null;
let wallsArr = [];
let redMenArr = [];
let heroPosObj = {
    x: (0 * 16) + 400,
    y: (-1 * (-20 * 16) + 400)
}

function setElementType(type) {
    console.log("Setting type: " + type);
    document.getElementById("label-type-id").innerHTML = "Mode type - " + type;
    typeSelected = type;
}

function modifyPosForUnity(x, y) {
    x -= 400;
    y -= 400;
    const xMeters = parseInt("" + (x / 16));
    const yMeters = parseInt("" + (y / 16));
    return {
        x: xMeters,
        z: -1 * yMeters
    };
}

function makeResultJson() {
    const resultObject = {
        arr: [],
        xHero: 0,
        zHero: 0
    };

    wallsArr.forEach(point => {
        resultObject.arr.push({
            type: "WALL_TYPE_ELEMENT",
            x: modifyPosForUnity(point.x, point.y).x,
            z: modifyPosForUnity(point.x, point.y).z
        });
    });

    redMenArr.forEach(point => {
        resultObject.arr.push({
            type: "RED_PERSON_TYPE_ELEMENT",
            x: modifyPosForUnity(point.x, point.y).x,
            z: modifyPosForUnity(point.x, point.y).z
        });
    });

    resultObject.xHero = modifyPosForUnity(heroPosObj.x, heroPosObj.y).x;
    resultObject.zHero = modifyPosForUnity(heroPosObj.x, heroPosObj.y).z;

    console.log("---------------------------------");
    console.log(JSON.stringify(resultObject, null, 4));
    console.log("---------------------------------");
}

window.onload = function () {
    const canvasElement = document.getElementById('can');
    const holst = canvasElement.getContext('2d');

    function drawCircleLevel() {
        holst.lineWidth = 8;
        holst.beginPath();
        const x = 400;
        const y = 400;
        const delta = 6;
        const radius = 400;
        const start = 0;
        const end = Math.PI * 2;
        const flag = true;
        holst.arc(x, y, radius - delta, start, end, flag);
        holst.closePath();
        holst.stroke();
    }

    function drawRectangle(x, y, size, color) {
        const half = size / 2;
        holst.fillStyle = color;
        holst.fillRect(x - half, y - half, size, size);
    }

    function renderContent() {
        const zero = 0;
        const sizeHolst = 800;
        holst.clearRect(zero, zero,sizeHolst, sizeHolst);
        drawCircleLevel();

        // default walls in level
        drawRectangle(320, 688, MAP_ELEMENT_SIZE, BASE_DEFAULT_WALLS_COLOR);
        drawRectangle(480, 688, MAP_ELEMENT_SIZE, BASE_DEFAULT_WALLS_COLOR);

        wallsArr.forEach(obj => {
            const {x, y} = obj;
            drawRectangle(x, y, MAP_ELEMENT_SIZE, WALL_COLOR);
        });

        redMenArr.forEach(obj => {
            const {x, y} = obj;
            drawRectangle(x, y, MAP_ELEMENT_SIZE, RED_PERSONS_COLOR);
        });

        if(heroPosObj) {
            drawRectangle(heroPosObj.x, heroPosObj.y, MAP_ELEMENT_SIZE, HERO_COLOR);
        }
    }

    renderContent();

    canvasElement.onclick = function (eventObject) {
        const xMouse = parseInt("" + eventObject.offsetX);
        const yMouse = parseInt("" + eventObject.offsetY);
        const pointObj = {x: xMouse, y: yMouse};

        if(typeSelected === 'TYPE_WALLS') {
            wallsArr.push(pointObj);
        } else if(typeSelected === 'TYPE_RED_PERSONS') {
            redMenArr.push(pointObj);
        } else if(typeSelected === 'TYPE_HERO') {
            heroPosObj = pointObj;
        }

        renderContent();
    };
};