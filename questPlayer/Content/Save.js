$(".answer-link").click(function (e) {
    alert($(this).data("result"));
    e.preventDefault();
});

var container = $("#viewDataContainer");
var myData = {
    questId: container.attr("data-questId"),
    questionId: container.attr("data-questionId")
}
console.log(myData);
localStorage.setItem('myData', JSON.stringify(myData));