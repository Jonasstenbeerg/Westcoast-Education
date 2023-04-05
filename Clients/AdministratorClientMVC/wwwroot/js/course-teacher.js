"use strict";
let buttons = document.querySelectorAll('.radiobutton');
let teacherId = document.querySelector('#teacherId')


buttons.forEach(button =>{
    
    if (button.checked) {
        teacherId.value=button.value;
    }
    
    button.addEventListener("change",() =>{
       
        teacherId.value=button.value;
        console.log(button.value)
    })
    
})


