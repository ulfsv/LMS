// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// OBS! OBS! OBS! OBS! OBS! Fixa document ready etc.

$(document).ready(function () {

    let courses = document.getElementsByClassName("courseSelector");

    for (var i = 0; i < courses.length; i++) {
        courses[i].addEventListener('click', getModuleList);
        courses[i].addEventListener('click', updateDetails);
    }
})

let localUrl = 'https://localhost:44360';

async function getModuleList() {
    let id = (this.id).substr(1);
    fetch(localUrl + '/Modules/GetModulesByCourse/' + id,
        {
            method: 'GET',
        })
        .then(res => res.text())
        .then(data => {
            moduleListContainer.innerHTML = data;
        })
        .catch(err => console.log(err));
};

function updateDetails() {
    let id = (this.id).substr(1);
    fetch(localUrl + '/Courses/PartialDetails/' + id,
        {
            method: 'GET',
        })
        .then(res => res.text())
        .then(data => {
            detailsContainer.innerHTML = data;
        })
        .catch(err => console.log(err));
};

