//star rating system js - this is the newer code for the star rating system Mar.20/20
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
$(document).ready(function () {

});
//star rating system - this is the first version
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

        var schoolId = $(this).val();
        $("#program").empty();
        $("#program").append("<option value=''>Select program</option>");
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
        $("#course").append("<option value=''>Select course</option>");
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
        $("#elective").append("<option value=''>Select elective</option>");
        $.getJSON(`?handler=Electives`, (data) => {
            $.each(data, function (i, item) {
                $("#elective").append(`<option value="${item.id}">${item.name}</option>`);
            });
        });
    });
});
