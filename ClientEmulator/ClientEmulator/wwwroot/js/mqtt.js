"use strict";

var connection = new signalR.HubConnectionBuilder()
    .withUrl("/mqtthub")
    .withAutomaticReconnect({
        nextRetryDelayInMilliseconds: retryContext => {
            return 5000;
        }
    })
    .build();

connection.on("Receive", function (device, command, value) {
    var list = document.getElementById("messages");
    var resp = device + " " + command + " " + value;
    var node = document.createElement("li");
    node.innerHTML = resp;
    list.appendChild(node);
});

connection.on("DeviceSend", function (device, command, value) {
    var list = document.getElementById("send-messages");
    var resp = device + " " + command + " " + value;
    var node = document.createElement("li");
    node.innerHTML = resp;
    list.appendChild(node);
});


connection.on("Connected", function (result) {
    var div = document.getElementById("send-device-id-result");
    if (result === "Success") {
        div.classList.add("bg-success");
        div.classList.remove("bg-danger");
        div.innerHTML = result;
    } else {
        div.classList.add("bg-danger");
        div.classList.remove("bg-success");
        div.innerHTML = result;
    }
    div.innerHTML = result;
});

connection.on("State", function (state, value) {
    var input = document.getElementById("state-" + state);
    input.value = value;
});


connection.start().then(function () {
    document.getElementById('signalr-status').innerHTML = 'connected';
}).catch(function (err) {
    document.getElementById('signalr-status').innerHTML = 'disconnected, reload browser to reconnect';
});

document.getElementById("send-device-id").addEventListener("click", function (event) {
    var div = document.getElementById("send-device-id-result");
    div.classList.remove('bg-success');
    div.classList.remove('bg-danger');
    div.innerHTML = '';
    var device = document.getElementById("device-id").value;
    var password = document.getElementById("password").value;
    connection.invoke("Connect", device, password).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

document.getElementById("send").addEventListener("click", function (event) {
    var command = document.getElementById("command").value;
    var value = document.getElementById("value").value;
    connection.invoke("Send", command, value).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

connection.onreconnecting((error) => {
    document.getElementById('signalr-status').innerHTML = `Connection lost due to error "${error}". Reconnecting.`;
});

connection.onreconnected((connectionId) => {
    document.getElementById('signalr-status').innerHTML = "connected";
});