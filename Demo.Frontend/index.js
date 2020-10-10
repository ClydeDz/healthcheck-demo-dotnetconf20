var apiBaseURL = "https://localhost:44310/";
var salesAPI = apiBaseURL + "sales";

$.get(salesAPI, function(data) {
    $("#customers").html(JSON.stringify(data));
});