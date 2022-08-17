function orderQuestionPage() {
    let questionHidden = document.getElementById("questionHidden");
    //let acceptingAnswers = true;
    /*    let selectedChoice;*/
    const maxQuestions = document.getElementById("maxQuestions").innerText;
    const availleblQuestions = document.getElementById("availleblQuestions").innerText;

    addEventListeners();

    function dragStart() {
        console.log("dragStart function");

        dragStartIndex = +this.closest('li').getAttribute('data-index');
        //submitAnswer.disabled = false;
    }

    function dragEnter() {
        console.log("dragEnter function");

        this.classList.add('over');
    }

    function dragLeave() {
        console.log("dragLeave function");
        this.classList.remove('over');
    }

    function dragOver(e) {
        console.log("dragOver function");
        e.preventDefault();
    }

    function dragDrop() {
        console.log("dragDrop function");
        const dragEndIndex = +this.getAttribute('data-index');
        swapItems(dragStartIndex, dragEndIndex);

        this.classList.remove('over');
    }

    // Swap list items that are drag and drop
    function swapItems(fromIndex, toIndex) {
        console.log("swapItems function");

        console.log(fromIndex);
        console.log(toIndex);

        const liList = document.getElementsByTagName("li");

        const itemOne = liList[fromIndex - 1].querySelector('.draggable');
        const itemTwo = liList[toIndex - 1].querySelector('.draggable');

        console.log(itemOne);
        console.log(itemTwo);

        liList[fromIndex - 1].appendChild(itemTwo);
        liList[toIndex - 1].appendChild(itemOne);
    }

    function addEventListeners() {
        const draggables = document.querySelectorAll('.draggable');
        const dragListItems = document.querySelectorAll('.draggable-list li');

        draggables.forEach(draggable => {
            draggable.addEventListener('dragstart', dragStart);
        });

        dragListItems.forEach(item => {
            item.addEventListener('dragover', dragOver);
            item.addEventListener('drop', dragDrop);
            item.addEventListener('dragenter', dragEnter);
            item.addEventListener('dragleave', dragLeave);
        });
    }

    // update the progress bar
    progressBarFull.style.width = `${(availleblQuestions / maxQuestions) * 100}%`;

    const currentOrder = [];
    const rightOrder = [];
    /// Check the order of list items
    checkOrder = () => {
        const liList = document.getElementsByTagName("li");
        let isTrue = 0;

        for (let i = 0; i < liList.length; i++) {

            currentOrder[i] = liList[i].getAttribute("data-index");
            rightOrder[i] = liList[i].querySelector('.draggable').getAttribute("RightOrder");

            if (currentOrder[i] == rightOrder[i]) {
                liList[i].querySelector('.draggable').classList.add("correct");
                isTrue++;
            }
            else {
                liList[i].querySelector('.draggable').classList.add("incorrect");

            }

            console.log("currentOrder: " + currentOrder[i] + "rightOrder: " + rightOrder[i]);
        }

        if (isTrue == 4) {
            questionHidden.value = true;
            var event = new Event('change');
            questionHidden.dispatchEvent(event);
        }
        else {
            questionHidden.value = false;
            var event = new Event('change');
            questionHidden.dispatchEvent(event);
        }

        setTimeout(() => {

            document.getElementById("saveOrderAnsDB").click();
        }, 1000);
    }

}

//function orderQuestionPage() {
//    let questionHidden = document.getElementById("questionHidden");
//    let submitAnswer = document.getElementById("submitAnswer");
//    //let acceptingAnswers = true;
//    let selectedChoice;
//    const maxQuestions = document.getElementById("maxQuestions").innerText;
//    const availleblQuestions = document.getElementById("availleblQuestions").innerText;

//    addEventListeners();

//    //answerClicked = (answer, e) => {

//    //    if (!acceptingAnswers) {
//    //        selectedChoice.parentElement.classList.remove("chosenAnswer");
//    //    }

//    //    acceptingAnswers = false;
//    //    questionHidden.value = answer;
//    //    var event = new Event('change');
//    //    questionHidden.dispatchEvent(event);

//    //    submitAnswer.disabled = false;

//    //    selectedChoice = e.target;
//    //    selectedChoice.parentElement.classList.add("chosenAnswer");

//    //}

//    function dragStart() {
//        console.log("dragStart function");

//        dragStartIndex = +this.closest('li').getAttribute('data-index');
//        submitAnswer.disabled = false;
//    }

//    function dragEnter() {

//            //function swapItems(fromIndex, toIndex) {
//            //    console.log(fromIndex);
//            //    console.log(toIndex);
//            //    console.log(li[fromIndex]);

//            //    const itemOne = li[fromIndex].querySelector('.draggable');
//            //    const itemTwo = li[toIndex].querySelector('.draggable');
//            //    const liList = document.getElementsByTagName("li");

//            //    const itemOne = liList[fromIndex - 1].querySelector('.draggable');
//            //    const itemTwo = liList[toIndex - 1].querySelector('.draggable');

//            //    li[fromIndex].appendChild(itemTwo);
//            //    li[toIndex].appendChild(itemOne);
//            //    console.log(itemOne);
//            //    console.log(itemTwo);

//            //    liList[fromIndex - 1].appendChild(itemTwo);
//            //    liList[toIndex - 1].appendChild(itemOne);
//            //}

//    //    function addEventListeners() {
//    //        @@ -83, 6 + 67, 49 @@
//    //        item.addEventListener('dragleave', dragLeave);
//    //    });
//    //}



//}



