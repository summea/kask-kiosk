﻿$(document).ready(function() {
    $.fn.fullpage({
        anchors: ['welcome', 'personal', 'position', 'jobHistory1', 'jobHistory2', 'jobHistory3', 'education1', 'education2', 'education3', 'submitApplication'],
        fixedElements: '#moveDown, #moveUp',
        keyboardScrolling: false,
        menu: '#menu',
        paddingBottom: '3em',
    });

    $('#moveDownExample').click(function () {
        $.fn.fullpage.moveSectionDown();
        return false;
    });

    $('#moveDown').click(function() {
        $.fn.fullpage.moveSectionDown();
    });

    $('#moveUp').click(function () {
        $.fn.fullpage.moveSectionUp();
    });
});