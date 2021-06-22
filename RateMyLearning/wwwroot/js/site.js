jQuery(document).ready(function ($) {
    var $star_rating = $('.star-rating .fa');

    var SetRatingStar = function () {
        return $star_rating.each(function () {
            if (parseInt($star_rating.siblings('input.rating-value').val()) >= parseInt($(this).data('rating'))) {
                return $(this).removeClass('fa-star-o').addClass('fa-star');
            } else {
                return $(this).removeClass('fa-star').addClass('fa-star-o');
            }
        });
    };

    $star_rating.on('click', function () {
        $star_rating.siblings('input.rating-value').val($(this).data('rating'));
        return SetRatingStar();
    });
    SetRatingStar();

    //accordion
    $(".open-button").on("click", function () {
        $(this).closest('.collapse-group').find('.collapse').collapse('show');
    });

    $(".close-button").on("click", function () {
        $(this).closest('.collapse-group').find('.collapse').collapse('hide');
    });

    // find and populate programs depending on whichever school was selected
    $('#school').change(function () {
        $('#program').prop('disabled', false);

        $("#program").empty();
        $("#program").append("<option value='none' selected disabled hidden>Select program</option>");
        $.getJSON(`?handler=Programs`, (data) => {
            $.each(data, function (i, item) {
                $("#program").append(`<option value="${item.id}">${item.name}</option>`);
            });
        });
    });

    // find and populate courses depending on whichever program was selected
    $('#program').change(function () {
        $('#course').prop('disabled', false);

        var programId = $(this).val();
        $("#course").empty();
        $("#course").append("<option value='none' selected disabled hidden>Select course</option>");
        $.getJSON(`?handler=Courses&programId=${programId}`, (data) => {
            $.each(data, function (i, item) {
                $("#course").append(`<option value="${item.id}">${item.name}</option>`);
            });
        });
    });

    // find and populate electives depending on whichever school was selected
    $('#elective-school').change(function () {
        $('#elective').prop('disabled', false);

        $("#elective").empty();
        $("#elective").append("<option value='none' selected disabled hidden>Select elective</option>");
        $.getJSON(`?handler=Electives`, (data) => {
            $.each(data, function (i, item) {
                $("#elective").append(`<option value="${item.id}">${item.name}</option>`);
            });
        });
    });

    // find and populate continuing education programs depending on whichever school was selected
    $('#continuing-education-school').change(function () {
        $('#continuing-education-program').prop('disabled', false);

        $("#continuing-education-program").empty();
        $("#continuing-education-program").append("<option value='none' selected disabled hidden>Select program</option>");
        $.getJSON(`?handler=ContinuingEducationPrograms`, (data) => {
            $.each(data, function (i, item) {
                $("#continuing-education-program").append(`<option value="${item.id}">${item.name}</option>`);
            });
        });
    });

    // find and populate continuing education courses depending on whichever program was selected
    $('#continuing-education-program').change(function () {
        $('#continuing-education-course').prop('disabled', false);

        var programId = $(this).val();
        $("#continuing-education-course").empty();
        $("#continuing-education-course").append("<option value='none' selected disabled hidden>Select course</option>");
        $.getJSON(`?handler=ContinuingEducationCourses&programId=${programId}`, (data) => {
            $.each(data, function (i, item) {
                $("#continuing-education-course").append(`<option value="${item.id}">${item.name}</option>`);
            });
        });
    });
});