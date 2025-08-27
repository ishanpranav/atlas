// index.js
// Copyright (c) 2025 Ishan Pranav
// Licensed under the MIT license.

window.onload = async function () {
    const inputInput = document.getElementById('inputInput');
    const outputTextarea = document.getElementById('ouputTextarea');
    const response = await fetch("https://localhost:7137/WeatherForecast");
    const data = await response.json();

    console.log(data);
};
