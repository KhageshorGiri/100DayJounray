
// Create a connection
var userCountConnection = new signalR.HubConnectionBuilder()
    .withUrl("/hubs/userCount")
    .build();
                                        
// Connect to method that hub invokes aks recivenotificactions form hubs
userCountConnection.on("UpdateTotalViews", (value) => {
    var newCountSpan = document.getElementById('totalViewCounter');
    newCountSpan.innerText = value.toString();
});

userCountConnection.on("UpdateTotalUserCount", (value) => {
    var newUserCountSpan = document.getElementById('totalUserCounter');
    newUserCountSpan.innerText = value.toString();
});

// invoke hub method aka send notification to hub
function newWindowLoadedOnClient() {
    userCountConnection.send("NewWindowLoaded");
}


// start connection
userCountConnection.start()
    .then(afterFulfilled, afterRejected);


// function to handle fulfilled condition
function afterFulfilled() {
    console.log("Connection to user hub sucessfull.");
    newWindowLoadedOnClient();
}

// function to handle rejected condition
function afterRejected() {

}