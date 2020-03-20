jQuery(document).ready(function ($) {
    $('.rating_stars span.r').hover(function () {
        // get hovered value
        var rating = $(this).data('rating');
        var value = $(this).data('value');
        $(this).parent().attr('class', '').addClass('rating_stars').addClass('rating_' + rating);
        highlight_star(value);
    }, function () {
        // get hidden field value
        var rating = $("#rating").val();
        var value = $("#rating_val").val();
        $(this).parent().attr('class', '').addClass('rating_stars').addClass('rating_' + rating);
        highlight_star(value);
    }).click(function () {
        // Set hidden field value
        var value = $(this).data('value');
        $("#rating_val").val(value);

        var rating = $(this).data('rating');
        $("#rating").val(rating);

        highlight_star(value);
    });

    var highlight_star = function (rating) {
        $('.rating_stars span.s').each(function () {
            var low = $(this).data('low');
            var high = $(this).data('high');
            $(this).removeClass('active-high').removeClass('active-low');
            if (rating >= high) $(this).addClass('active-high');
            else if (rating == low) $(this).addClass('active-low');
        });
    }

    // find and populate programs depending on whichever school was selected
    $('#school').change(function () {
        $('#program').prop('disabled', false);

        var schoolId = $(this).val();
        $("#program").empty();
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
        $.getJSON(`?handler=Courses&programId=${programId}`, (data) => {
            $.each(data, function (i, item) {
                $("#course").append(`<option value="${item.id}">${item.name}</option>`);
            });
        });
    });

    // find and populate electives depending on whichever school was selected
    $('#elective-school').change(function () {
        $('#elective').prop('disabled', false);

        var schoolId = $(this).val();
        $("#elective").empty();
        $.getJSON(`?handler=Electives`, (data) => {
            $.each(data, function (i, item) {
                $("#elective").append(`<option value="${item.id}">${item.name}</option>`);
            });
        });
    });

    // find and populate continuing education programs depending on whichever school was selected
    $('#continuing-education-school').change(function () {
        $('#continuing-education-program').prop('disabled', false);

        var schoolId = $(this).val();
        $("#continuing-education-program").empty();
        $("#continuing-education-program").append("<option value=''>Select program</option>");
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
        $.getJSON(`?handler=ContinuingEducationCourses&programId=${programId}`, (data) => {
            $.each(data, function (i, item) {
                $("#continuing-education-course").append(`<option value="${item.id}">${item.name}</option>`);
            });
        });
    });
});