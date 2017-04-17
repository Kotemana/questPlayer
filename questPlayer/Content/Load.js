$(document).ready(function () {
    console.log("document loaded");
});
var retrieveMyData = localStorage.getItem('myData');
var data = JSON.parse(retrieveMyData);
var url = "/Quest/Question?questId=" + data.questId + "&questionId=" + data.questionId + "&startNew=True";
if (data.questId || data.questId != null) {
    var tag = "<a href='" + url + "'> Continue </a>";
    document.getElementById("Continue").innerHTML = tag;
}