var apiBaseURL = "https://adventureworks7941.azurewebsites.net/api/";
var salesAPI = apiBaseURL + "sales";

$.get(salesAPI, function(data) {
    $("#customers").html(JSON.stringify(data));
});