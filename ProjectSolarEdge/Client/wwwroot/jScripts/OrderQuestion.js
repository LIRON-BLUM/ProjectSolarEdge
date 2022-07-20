function orderQuestionPage() {
    let questionHidden = document.getElementById("questionHidden");
    let submitAnswer = document.getElementById("submitAnswer");
    //let acceptingAnswers = true;
    let selectedChoice;

    addEventListeners();

    //answerClicked = (answer, e) => {

    //    if (!acceptingAnswers) {
    //        selectedChoice.parentElement.classList.remove("chosenAnswer");
    //    }

    //    acceptingAnswers = false;
    //    questionHidden.value = answer;
    //    var event = new Event('change');
    //    questionHidden.dispatchEvent(event);

    //    submitAnswer.disabled = false;

    //    selectedChoice = e.target;
    //    selectedChoice.parentElement.classList.add("chosenAnswer");

    //}

    function dragStart() {
        console.log("dragStart function");
        
        dragStartIndex = +this.closest('li').getAttribute('data-index');
        submitAnswer.disabled = false;
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
        console.log(fromIndex);
        console.log(toIndex);
        console.log(li[fromIndex]);

        const itemOne = li[fromIndex].querySelector('.draggable');
        const itemTwo = li[toIndex].querySelector('.draggable');

        li[fromIndex].appendChild(itemTwo);
        li[toIndex].appendChild(itemOne);
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
}

//const draggable_list = document.getElementById('draggable-list');
//const submitAnswer = document.getElementById('checkAnswer');


//// Store listitems
//const listItems = [];
//let dragStartIndex;

//createList();


//// Insert list items into DOM
//function createList() {
//    //correntQuestion = availableQuesions[questionCount];

//    //correntQuestion.listItem
//    //    .map(a => ({ value: a, sort: Math.random() }))
//    //    .sort((a, b) => a.sort - b.sort)
//    //    .map(a => a.value)
//    //    .forEach((person, index) => {
//    //        const listItem = document.createElement('li');

//    //        listItem.setAttribute('data-index', index);

//    //        listItem.innerHTML = `
//    //         <div class="draggable" draggable="true">
//    //         <p class="person-name">${person}</p>
//    //         <i class="fas fa-grip-lines"></i>
//    //         </div>
//    //         `;

//    //        listItems.push(listItem);

//    //        draggable_list.appendChild(listItem);
//    //    });

//    addEventListeners();
//}



//// Check the order of list items
//    checkOrder = () => {
//    listItems.forEach((listItem, index) => {
//        const personName = listItem.querySelector('.draggable').innerText.trim();

//        //if (personName !== questions[index]) {
//        if (personName !== correntQuestion.listItem[index]) {
//            listItem.classList.add('wrong');
//        } else {
//            listItem.classList.remove('wrong');
//            listItem.classList.add('right');
//        }
//    });
//    questionCount++
//        setTimeout(() => {
//            const  removedIdems = document.getElementsByClassName("draggable");
//          //  draggable_list.removeChild(removedIdems);

//          createList();
//    }, 1000);
//    }



