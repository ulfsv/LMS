// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

let courses = document.getElementsByClassName("courseSelector");

for (var i = 0; i < courses.length; i++) {
    courses[i].addEventListener('click', getModuleList);
}

let elementToUpdate = document.querySelector('#moduleListContainer');

async function getModuleList() {
    let id = (this.id).substr(1);
    fetch('https://localhost:44360/Modules/GetModulesByCourse/' + id,
        {
            method: 'GET',
        })
        .then(res => res.text())
        .then(data => {
            moduleListContainer.innerHTML = data;
        })
        .catch(err => console.log(err));
};