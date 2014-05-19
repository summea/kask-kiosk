$(document).ready(function () {
    $.fn.fullpage({
        anchors: ['welcome', 'interviewQuestions', 'submitInterview'],
        fixedElements: '#moveDown, #moveUp',
        keyboardScrolling: false,
        menu: '#menu',
        paddingBottom: '3em',
    });

    $('#moveDownExample').click(function () {
        $.fn.fullpage.moveSectionDown();
        return false;
    });

    $('#moveDown').click(function () {
        $.fn.fullpage.moveSectionDown();
    });

    $('#moveUp').click(function () {
        $.fn.fullpage.moveSectionUp();
    });

    // get JobOpeningIDReferenceNumber, if it exists in URL querystring
    var JobOpeningIDReferenceNumber = window.location.search.substring(1).split('=')[1];

    // put JobOpeningIDReferenceNumber into the readonly text field on the "Application" screen
    $('#JobOpeningIDReferenceNumber').val(JobOpeningIDReferenceNumber);
});