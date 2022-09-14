function initiateDrag() {
   /* window.alert("In");*/
    console.log("in");
    const draggable = new Draggable.Sortable(document.querySelectorAll('ul'), {
        draggable: 'li'
    });


    draggable.on('sortable:start', SortStart);
    draggable.on('sortable:sort', () => console.log('sortable:sort'));
    draggable.on('sortable:sorted', SortSorted);
    draggable.on('sortable:stop', SortStop);

    function SortStart() {
        console.log("Sort Start");


        console.log(draggable);
    }

    function SortSorted() {
        console.log(draggable);
        const dragEndIndex = +this.getAttribute('data-index');
        swapItems(dragStartIndex, dragEndIndex);

        this.classList.remove('over');
    }

    function SortStop() {
        console.log(draggable);
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
}