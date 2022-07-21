function GameElements() {

    const maxQuestions = document.getElementById("maxQuestions").innerText;
    const availleblQuestions = document.getElementById("availleblQuestions").innerText;

    // update the progress bar
    progressBarFull.style.width = `${(availleblQuestions / maxQuestions) * 100}%`;

}