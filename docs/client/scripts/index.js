// index.js
// Copyright (c) 2025 Ishan Pranav
// Licensed under the MIT license.

//const baseUrl = 'https://ishanpranav-atlas.onrender.com/';
const baseUrl = 'https://localhost:7137/';

const inputInput = document.getElementById('inputInput');
const outputTextarea = document.getElementById('outputTextarea');
const mainForm = document.getElementById('mainForm');

let username = null;
let password = null;

window.onload = async function () {
    const response = await fetch(baseUrl + 'Application');
    const data = await response.json();

    handleRefresh(data);
};

mainForm.addEventListener('submit', async function (e) {
    e.preventDefault();

    if (!inputInput.value) {
        return;
    }

    let value = null;

    if (!username) {
        username = inputInput.value.trim();
    } else if (!password) {
        password = inputInput.value.trim();
    } else {
        value = inputInput.value.trim();
    }

    const response = await fetch(baseUrl + 'Application', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            username: username,
            password: password,
            value: value
        })
    });
    const data = await response.json();

    handleRefresh(data);
})

function handleRefresh(data) {
    outputTextarea.value = data.value;

    if (data.isNextPassword) {
        inputInput.type = 'password';
    } else {
        inputInput.type = 'text';
    }

    inputInput.value = null;
}
