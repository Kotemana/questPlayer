$(".answer-link").click(function () {
    alert($(this).data("result"));
});

var container = $("#viewDataContainer");
var myData = {
    questId: container.attr("data-questId"),
    questionId: container.attr("data-questionId")
}
console.log(myData);
localStorage.setItem('myData', JSON.stringify(myData));