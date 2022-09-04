function orderQuestionPage() {

    /**
 * Lightweight script to convert touch handlers to mouse handlers
 * credit: http://stackoverflow.com/a/6141093
 */
    //(function () {
    //    function touchHandler(e) {
    //        var touches = e.changedTouches;
    //        var first = touches[0];
    //        var type = "";

    //        switch (e.type) {
    //            case "touchstart":
    //                type = "mousedown";
    //                break;
    //            case "touchmove":
    //                type = "mousemove";
    //                break;
    //            case "touchend":
    //                type = "mouseup";
    //                break;
    //            default:
    //                return;
    //        }

    //        var simulatedEvent = document.createEvent("MouseEvent");
    //        simulatedEvent.initMouseEvent(type, true, true, window, 1, first.screenX, first.screenY, first.clientX, first.clientY, false, false, false, false, 0, null);

    //        first.target.dispatchEvent(simulatedEvent);
    //        e.preventDefault();
    //    }

    //    function init() {
    //        document.addEventListener("touchstart", touchHandler, true);
    //        document.addEventListener("touchmove", touchHandler, true);
    //        document.addEventListener("touchend", touchHandler, true);
    //        document.addEventListener("touchcancel", touchHandler, true);
    //    }

    //    init();
    //})();


    let questionHidden = document.getElementById("questionHidden");
    //let acceptingAnswers = true;
    /*    let selectedChoice;*/
    const maxQuestions = document.getElementById("maxQuestions").innerText;
    const availleblQuestions = document.getElementById("availleblQuestions").innerText;


    addEventListeners();
    random();

    //function touchHandler(event) {
    //    var touches = event.changedTouches,
    //        first = touches[0],
    //        type = "";

    //    switch (event.type) {
    //        case "touchstart": type = "mousedown"; break;
    //        case "touchmove": type = "mousemove"; break;
    //        case "touchend": type = "mouseup"; break;
    //        default: return;
    //    }

    //    // initMouseEvent(type, canBubble, cancelable, view, clickCount,
    //    //                screenX, screenY, clientX, clientY, ctrlKey,
    //    //                altKey, shiftKey, metaKey, button, relatedTarget);

    //    var simulatedEvent = document.createEvent("MouseEvent");
    //    simulatedEvent.initMouseEvent(type, true, true, window, 1,
    //        first.screenX, first.screenY,
    //        first.clientX, first.clientY, false,
    //        false, false, false, 0/*left*/, null);

    //    first.target.dispatchEvent(simulatedEvent);
    //    event.preventDefault();
    //}

    //function init() {
    //    document.addEventListener("touchstart", touchHandler, true);
    //    document.addEventListener("touchmove", touchHandler, true);
    //    document.addEventListener("touchend", touchHandler, true);
    //    document.addEventListener("touchcancel", touchHandler, true);
    //}

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

    (function () {
        function touchHandler(e) {
            var touches = e.changedTouches;
            var first = touches[0];
            var type = "";

            switch (e.type) {
                case "touchstart":
                    type = "mousedown";
                    break;
                case "touchmove":
                    type = "mousemove";
                    break;
                case "touchend":
                    type = "mouseup";
                    break;
                default:
                    return;
            }

            var simulatedEvent = document.createEvent("MouseEvent");
            simulatedEvent.initMouseEvent(type, true, true, window, 1, first.screenX, first.screenY, first.clientX, first.clientY, false, false, false, false, 0, null);

            first.target.dispatchEvent(simulatedEvent);
            e.preventDefault();
        }

        function init() {
            document.addEventListener("touchstart", touchHandler, true);
            document.addEventListener("touchmove", touchHandler, true);
            document.addEventListener("touchend", touchHandler, true);
            document.addEventListener("touchcancel", touchHandler, true);
        }

        init();
    })();
}



