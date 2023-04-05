"use strict";

const submitButton = document.querySelector('.submit-button');
const selectBox = document.querySelector('.header-searchbar');


submitButton.addEventListener('click',(e)=>{
   
    if(selectBox.value=='inget')
    {
        
        e.preventDefault();
    }
})
