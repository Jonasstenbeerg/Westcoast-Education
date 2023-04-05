"use strict";

const submitButton = document.querySelector('#search-button');
const selectBox = document.querySelector('#search-select');


submitButton.addEventListener('click',(e)=>{
    
    if(selectBox.value=='inget')
    {
        
        e.preventDefault();
    }
})
