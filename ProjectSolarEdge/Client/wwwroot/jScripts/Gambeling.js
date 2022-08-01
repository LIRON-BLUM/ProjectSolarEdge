function gambelingPage() {

const gambelOptions = document.getElementById('gambelOptions');
const gambelContinue = document.getElementById('gambelContinue');
let choosenGambel = document.getElementById('choosenGambel');
let gambelScoreHidden = document.getElementById('gambelScoreHidden');
//let score = +localStorage.getItem("mostRecentScore");
let newScore = document.getElementById('colectedScore');

newScore.innerText = "score: " + 200;

    gambelChoice = () => {
    let gambelChoice = gambelOptions.options[gambelOptions.selectedIndex].text
    if (score >= +gambelChoice) {
        choosenGambel.innerText = gambelOptions.options[gambelOptions.selectedIndex].text;
        gambelScoreHidden.value = gambelOptions.options[gambelOptions.selectedIndex].text;
        var event = new Event('change');
        gambelScoreHidden.dispatchEvent(event);

        gambelContinue.disabled = false;
    }
    else {
        choosenGambel.innerText = "not enough points";
        gambelContinue.disabled = true;
    }
    
    }
}