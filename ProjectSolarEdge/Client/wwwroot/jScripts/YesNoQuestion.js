function YesNoQuestion() {
        let questionHidden = document.getElementById("questionHidden");
        let submitAnswer = document.getElementById("submitAnswer");
        let acceptingAnswers = true;
        let selectedChoice;

        answerClicked = (answer, e) => {

            if (!acceptingAnswers) {
                selectedChoice.parentElement.classList.remove("chosenAnswer");
            }

            acceptingAnswers = false;
            questionHidden.value = answer;
            var event = new Event('change');
            questionHidden.dispatchEvent(event);

            submitAnswer.disabled = false;

            selectedChoice = e.target;
            selectedChoice.parentElement.classList.add("chosenAnswer");

        }
    
}
////    const question = document.getElementById("question");
////    const submitAnswer = document.getElementById("submitAnswer");
////    const choices = Array.from(document.getElementsByClassName("choice-text"));
////    //const scoreText = document.getElementById("score");

////    let currentQuestion = {};
////    let acceptingAnswers = false;
////    //let score = 0;
////    let questionCounter = 0;
////    let availableQuesions = [];

////   let questions = [
////        {
////            question: "Does Liron miss me?",
////            QuestionPic: "",
////            choice1: "yes",
////            choice2: "no",
////            answer: 2,
////            feedback: "no she's made",
////            tyep: "yes/no",
////            difficulty: 1
////        },
////        {
////            question: "Are we going to get 100?",
////            choice1: "yes",
////            choice2: "no",
////            answer: 1,
////            feedback: "you bet we are",
////            tyep: "yes/no",
////            difficulty: 2
////       }
////    ]

////    //CONSTANTS
////    const CORRECT_BONUS = 10;
////    const MAX_QUESTIONS = questions.length;

////    startGame = () => {
////        questionCounter = 0;
////        score = 0;
////        availableQuesions = [...questions];
////        getNewQuestion();
////    };

////    getNewQuestion = () => {
////        if (availableQuesions.length === 0 || questionCounter >= MAX_QUESTIONS) {
////            //go to the end page
////        //    return window.location.assign("/end.html");
////            console.log("end");
////        }
////        questionCounter++;

////        //random
////        const questionIndex = Math.floor(Math.random() * availableQuesions.length);
////        currentQuestion = availableQuesions[questionIndex];
////        question.innerText = currentQuestion.question;     

////        choices.forEach(choice => {
////            const number = choice.dataset['number'];
////                choice.innerText = currentQuestion['choice' + number];
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