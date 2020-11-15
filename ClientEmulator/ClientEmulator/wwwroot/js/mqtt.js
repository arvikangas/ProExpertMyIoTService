"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/mqtthub").build();

//Disable send button until connection is established
document.getElementById("send").disabled = true;

connection.on("Receive", function (device, command, value) {
    var list = document.getElementById("messages");
    var resp = device + " " + command + " " + value;
    var node = document.createElement("li");
    node.innerHTML = resp;
    list.appendChild(node);
});

connection.start().then(function () {
    document.getElementById("send").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("send").addEventListener("click", function (event) {
    var device = document.getElementById("device").value;
    var command = document.getElementById("command").value;
    var value = document.getElementById("value").value;
    connection.invoke("Send", device, command, value).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});