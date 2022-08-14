﻿function orderQuestionPage() {
    let questionHidden = document.getElementById("questionHidden");
    const maxQuestions = document.getElementById("maxQuestions").innerText;
    const availleblQuestions = document.getElementById("availleblQuestions").innerText;

    addEventListeners();

    function dragStart() {
        console.log("dragStart function");
        
        dragStartIndex = +this.closest('li').getAttribute('data-index');
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

        const liList = document.getElementsByTagName("li");

        const itemOne = liList[fromIndex-1].querySelector('.draggable');
        const itemTwo = liList[toIndex-1].querySelector('.draggable');

        console.log(itemOne);
        console.log(itemTwo);

        liList[fromIndex-1].appendChild(itemTwo);
        liList[toIndex-1].appendChild(itemOne);
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
        for (let i = 0; i < liList.length; i++ ) {

        
            currentOrder[i] = liList[i].getAttribute("data-index");
            rightOrder[i] = liList[i].querySelector('.draggable').getAttribute("RightOrder");

            if (currentOrder[i] == rightOrder[i]) {
                liList[i].querySelector('.draggable').classList.add("correct");
                isTrue++;
            }
            else {
                liList[i].querySelector('.draggable').classList.add("incorrect");

            }
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



