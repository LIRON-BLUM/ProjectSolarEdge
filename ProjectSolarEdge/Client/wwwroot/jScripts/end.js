function endPage() {

const saveScoreBtn = document.getElementById('saveScoreBtn');
const finalScore = document.getElementById('finalScore');
const mostRecentScore = localStorage.getItem("mostRecentScore");
const highScores = JSON.parse(localStorage.getItem("highScores")) || [];
const userName = JSON.parse(localStorage.getItem("userName")) || [];


finalScore.innerText = mostRecentScore;

const score = {
    score: mostRecentScore,
    name: userName,
    location: highScores.length
};
highScores.push(score);
localStorage.setItem("highScores", JSON.stringify(highScores));

saveHighScore = (e) => {
    e.preventDefault();   
    window.location.assign("OpeningPage.html");
}

// leaderboard //
const userLucationList = document.getElementById('userLucationList');

//let num = [];
//for (let itemNum = 0; itemNum < highScores.length; itemNum++ ) {
//    num[itemNum] = itemNum;
//}

//let playerLocation = highScores.findIndex(
//    element => element.score === mostRecentScore && element.name === userName && element.location === highScores.length);
//alert(playerLocation);

highScores.sort((a, b) => b.score - a.score);
highScores.splice(5);
userLucationList.innerHTML = highScores
    .map(score => {
        return `<li class="high-score"> ${score.location}. ${score.name} - ${score.score}</li>`;
    })
    .join("");

//userLucationList.innerHTML = highScores
//    .map(score => {
//        return `<tr class="high-score"> 
//                <td> ${score.location}. </td>
//                <td> ${score.name} </td>
//                <td>${score.score}</td>
//                </tr>`;
//    })
//    .join("");


// three best //
const highScoreList = document.getElementById('highScoreList2');

highScores.sort((a, b) => b.score - a.score);
highScores.splice(3);
highScoreList.innerHTML = highScores
    .map(score => {
        return `<li class="high-score">${score.name} - ${score.score}</li>`;
    })
    .join("");

}