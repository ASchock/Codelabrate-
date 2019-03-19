var connection = new signalR.HubConnectionBuilder().withUrl("/session").build();

connection.start().catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("startSession").addEventListener("click", function (event) {
    
    var message = document.getElementById("messageInput").value;
    connection.invoke("SendMessage", user, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});