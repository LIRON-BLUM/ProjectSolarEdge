function logInPage() {

const GameCode = document.getElementById('GameCode');
const employeeEmail = document.getElementById('employeeEmail');
const StartGame = document.getElementById('StartGame');
const ErrorHelp = document.getElementById('ErrorHelp');

    employeeEmail.addEventListener('keyup', () => {
        GameCode.addEventListener('keyup', () => {
            StartGame.disabled = !employeeEmail.value;

            employeeEmail.value = employeeEmail.value;
            var employeeEvent = new Event('change');
            employeeEmail.dispatchEvent(employeeEvent);

            GameCode.value = GameCode.value;
            var CodeEvent = new Event('change');
            GameCode.dispatchEvent(CodeEvent);
    });
});



StartTheGame = (e) => {

    e.preventDefault();

    if (GameCode.value != null && !employeeEmail.value != null) {

        //window.location.assign('/OpeningPage/3');


        //localStorage.setItem("userName", JSON.stringify(GameCode.value));
        //localStorage.setItem("mostRecentScore", 0);
        //localStorage.setItem("gameStarted", 1);
        //localStorage.setItem("timeLeft", 60);
    }
    else {
        ErrorHelp.innerText = "Plese enter your full name and Email";
    }
}
}