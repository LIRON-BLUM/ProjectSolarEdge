function MultipelQuestion() {
    let questionHidden = document.getElementById("questionHidden");
    let submitAnswer = document.getElementById("submitAnswer");
    let acceptingAnswers = true;
    let selectedChoice;
    let isRight;
    const maxQuestions = document.getElementById("maxQuestions").innerText;
    const availleblQuestions = document.getElementById("availleblQuestions").innerText;

    answerClicked = (answer, e) => {

        if (!acceptingAnswers)
        {
            selectedChoice.parentElement.classList.remove("chosenAnswer");
        }

        acceptingAnswers = false;
        questionHidden.value = answer;
        isRight = answer;
        var event = new Event('change');
        questionHidden.dispatchEvent(event);

        //submitAnswer.disabled = false;

        selectedChoice = e.target;
        selectedChoice.parentElement.classList.add("chosenAnswer");
       
    }

    // update the progress bar
    progressBarFull.style.width = `${(availleblQuestions / maxQuestions) * 100}%`;

    //submit= () => {
    //    if (isRight == true) {
    //        selectedChoice.parentElement.classList.add("correct");

    //    }
    //    else {
    //        selectedChoice.parentElement.classList.add("chosenAnswer");
    //    }


    //    setTimeout(() => {

    //    document.getElementById("submitAnswer").click();
    //    }, 1000);
    //}

}


     

////        choices.forEach(choice => {
////            const number = choice.dataset['number'];

////            if (currentQuestion['choice' + number] == "") {

////                const choiceImg = document.createElement("img");
////                choiceImg.src = currentQuestion['choiceImg' + i];
////                choiceImg.setAttribute("style", "width:150px");
////                choice.appendChild(choiceImg);
////            }
////            else {
////                choice.innerText = currentQuestion['choice' + number];
////            }
////        });

////        choices.forEach(choice => {
////            choice.addEventListener("click", e => {
////                if (!acceptingAnswers) {
////                    selectedChoice.parentElement.classList.remove("chosenAnswer");
////                }

////                acceptingAnswers = false;
////                selectedChoice = e.target;
////                selectedAnswer = choice.dataset['number'];

////                selectedChoice.parentElement.classList.add("chosenAnswer");
////                submitAnswer.disabled = false;
////            });
////        });
////         availableQuesions.splice(questionIndex, 1);
////        acceptingAnswers = true;
////    };

////    saveAnawer = (e) => {
        
////        submitAnswer.disabled = true;
////        const classToApply = selectedAnswer == currentQuestion.answer ? "correct" : "incorrect";

////        if (classToApply == "correct") {
////            incremenScore(CORRECT_BONUS);
////        }
////        else {
////            decremenScore(CORRECT_BONUS);
////        }
////        selectedChoice.parentElement.classList.remove("chosenAnswer");

////        questionCounter++;

////        selectedChoice.parentElement.classList.add(classToApply);

////        setTimeout(() => {
////            selectedChoice.parentElement.classList.remove(classToApply);

////            if (questionCounter == MAX_QUESTIONS) {
////                //localStorage.setItem("mostRecentScore", score);
////                //return window.location.assign('/end');
////                console.log("end");

////            }
////            else {
////                //window.location.assign('/WheelOfFortune');
////                getNewQuestion();
////            }
////        }, 1000);

////    }
 
////    incremenScore = num => {
////        //score += num;
////        //scoreText.innerText = score;
////    };

////    decremenScore = num => {
////        //score += num;
////        //scoreText.innerText = score;
////    };


////    startGame();
////}