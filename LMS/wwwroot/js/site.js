// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// OBS! OBS! OBS! OBS! OBS! Fixa document ready etc.

$(document).ready(function () {
    let courses = document.getElementsByClassName("courseSelector");
    for (var i = 0; i < courses.length; i++) {
        courses[i].addEventListener('click', getModuleList);
        courses[i].addEventListener('click', updateCourseDetails);
    }
})


/* local host */
let localUrl = 'https://localhost:44360';

async function getModuleList() {
    let id = (this.id).substr(1);
    fetch(localUrl + '/Modules/GetModulesByCourse/' + id,
        {
            method: 'GET',
        })
        .then(res => res.text())
        .then(data => {

            //let temp = $('<div/>').html(data);
            //temp.find('.activitySelector').addEventListener('click', updateActivityDetails);

            moduleListContainer.innerHTML = data;



            /*for loop for activity details function */
            let activities = document.getElementsByClassName("activitySelector");
            for (var i = 0; i < activities.length; i++) {
                activities[i].addEventListener('click', updateActivityDetails);
            }

            let modules = document.getElementsByClassName("moduleSelector");
            for (var i = 0; i < modules.length; i++) {
                modules[i].addEventListener('click', updateModuleDetails);
            }
        })
        .catch(err => console.log(err));
};

/* Course Details */
function updateCourseDetails() {
    let id = (this.id).substr(1);
    fetch(localUrl + '/Courses/PartialDetails/' + id,
        {
            method: 'GET',
        })
        .then(res => res.text())
        .then(data => {
            courseDetailsContainer.innerHTML = data;
            detailsContainer.innerHTML = "";

        })
        .catch(err => console.log(err));
};

/*Activity Details */
function updateActivityDetails() {
    let id = (this.id).substr(1);
    fetch(localUrl + '/Activities/PartialDetails/' + id,
        {
            method: 'GET',
        })
        .then(res => res.text())
        .then(data => {
            detailsContainer.innerHTML = data;
        })
        .catch(err => console.log(err));
};

function updateModuleDetails() {
    let id = (this.id).substr(1);
    fetch(localUrl + '/Modules/PartialDetails/' + id,
        {
            method: 'GET',
        })
        .then(res => res.text())
        .then(data => {
            detailsContainer.innerHTML = data;

        })
        .catch(err => console.log(err));
};
