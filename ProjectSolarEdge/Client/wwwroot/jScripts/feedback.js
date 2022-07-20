function endFeedbackPage() {

var questions = document.getElementsByClassName("accordion");
var i;

for (i = 0; i < questions.length; i++) {
    questions[i].addEventListener("click", function () {
        this.classList.toggle("active");
        var panel = this.nextElementSibling;
        if (panel.style.display === "block") {
            panel.style.display = "none";
        } else {
            panel.style.display = "block";
        }
    });
}

}