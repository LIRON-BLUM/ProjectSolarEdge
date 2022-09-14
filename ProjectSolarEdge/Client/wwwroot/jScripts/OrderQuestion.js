
function orderQuestionPage() {


    let questionHidden = document.getElementById("questionHidden");
    //let acceptingAnswers = true;
    /*    let selectedChoice;*/
    const maxQuestions = document.getElementById("maxQuestions").innerText;
    const availleblQuestions = document.getElementById("availleblQuestions").innerText;

    random();

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

        liList[fromIndex - 1].appendChild(itemTwo);
        liList[toIndex - 1].appendChild(itemOne);
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

            console.log("currentOrder: " + currentOrder[i]);
            console.log("rightOrder: " + rightOrder[i]);
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





    function random() {
        const liList = document.getElementsByTagName("li");

        const answersNum = 4;
        let CorrentPosition;
        for (let i = 0; i < answersNum; i++) {

            CorrentPosition = (Math.floor(Math.random() * answersNum));

            const itemOne = liList[i].querySelector('.draggable');
            const itemTwo = liList[CorrentPosition].querySelector('.draggable');


            liList[i].appendChild(itemTwo);
            liList[CorrentPosition].appendChild(itemOne);
        }
    }

}





