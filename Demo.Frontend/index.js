var apiBaseURL = "http://adventureworks7942.trafficmanager.net/api/";
var salesAPI = apiBaseURL + "sales";

$.get(salesAPI, function(data) {
    $("#customers").html(JSON.stringify(data));
});