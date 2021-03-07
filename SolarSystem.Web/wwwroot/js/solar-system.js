const PI = Math.PI;
const color = {
    Green: "#339933",
    Blue: "#0050FF",
    Gray: "#AABBCC"
};
const RandomPlanetsData = {
    Ferrengi: {
        size: 50,
        color: color.Blue
    },
    Vulcano: {
        size: 40,
        color: color.Green
    },
    Betasoide: {
        size: 45,
        color: color.Gray
    }
};
const center = {};
const kilometersToPixels = kmSize => kmSize / 5;
class GameInterface {
    constructor() {
        this.canvas = document.getElementById("my-canvas");
        this.context = this.canvas.getContext("2d");
        center.x = this.canvas.width / 2;
        center.y = this.canvas.height / 2;
    }
    drawPlanet = (planet) => {
        this.context.beginPath();
        this.context.fillStyle = planet.color;
        this.context.arc(planet.x, planet.y, kilometersToPixels(planet.size), 0, 2 * PI);
        this.context.fill();
        
        this.context.font = "15px Arial";
        this.context.fillStyle = 'black';
        this.context.textAlign = "center";
        this.context.fillText(planet.name, planet.x, planet.y);

        this.context.closePath();
    }
    drawPlanetOrbit = (distanceToSun) => {
        this.context.beginPath();
        this.context.arc(center.x, center.y, kilometersToPixels(distanceToSun), 0, 2 * PI);
        this.context.stroke();
        this.context.closePath();
    }
    drawLine = (x1, y1, x2, y2) => {
        this.context.beginPath();
        this.context.moveTo(x1, y1);
        this.context.lineTo(x2, y2);
        this.context.stroke();
        this.context.closePath();
    }
    drawState = (planets, planetPositions) => {
        this.context.clearRect(0, 0, this.canvas.width, this.canvas.height);
        const solarSystemState = Object.keys(planetPositions).reduce((acc, item, index) => ({
            ...acc,
            [item]: {
                x: center.x + kilometersToPixels(planetPositions[item].x),
                y: center.y + kilometersToPixels(planetPositions[item].y),
                ...planets.find(p => p.name === item),
                ...RandomPlanetsData[item]
            }
        }), {});
        const planetKeys = Object.keys(solarSystemState);
        planetKeys.forEach(planetKey => {
            const planet = solarSystemState[planetKey];
            this.drawPlanetOrbit(planet.distanceToSunInKm);
            this.drawPlanet(planet);
        });
        for (let i = 0; i < planetKeys.length; i++) {
            const currentPlanet = solarSystemState[planetKeys[i]];
            const nextPlanet = solarSystemState[planetKeys[(i + 1) % planetKeys.length]];
            this.drawLine(currentPlanet.x, currentPlanet.y, nextPlanet.x, nextPlanet.y);
        }
    }
}

const gameInterface = new GameInterface();
let currentDay = 0;
const form = document.getElementById('form');
form.dayInput = form.querySelector('#day-input');
form.nextDay = form.querySelector('#next-day');
const selectedDayLabel = document.getElementById('current-day');

const getConditionsForDay = day => {
    currentDay = day;
    return fetch(`${location.origin}/condiciones?dia=${day}`).then(rawReponse => rawReponse.json())
        .then(res => {
            console.log(res);
            gameInterface.drawState(res.planets, res.planetPositions);
            selectedDayLabel.innerHTML = `Dia Actual: ${currentDay}, Tipo de clima: ${res.weather}`;
        });
}
    

const getConditionsNextDay = () => {
    currentDay++;
    getConditionsForDay(currentDay);
}

form.nextDay.addEventListener('click', getConditionsNextDay);
form.addEventListener('submit', e => {
    e.preventDefault();
    if (form.dayInput.value !== "") getConditionsForDay(form.dayInput.value);
});

window.onload = () => {
    getConditionsNextDay();
}

