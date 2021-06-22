jQuery(document).ready(function ($) {
    // find and populate programs depending on whichever school was selected
    $('#f-pc-school').change(function () {
        $('#f-pc-program').prop('disabled', false);

        $("#f-pc-program").empty();
        $("#f-pc-program").append("<option value='none' selected disabled hidden>Select program</option>");
        $.getJSON(`?handler=AllPrograms`, (data) => {
            $.each(data, function (i, item) {
                $("#f-pc-program").append(`<option value="${item.id}">${item.name}</option>`);
            });
        });
    });

    // find and populate courses depending on whichever program was selected
    $('#f-pc-program').change(function () {
        $('#f-pc-course').prop('disabled', false);

        var programId = $(this).val();
        $("#f-pc-course").empty();
        $("#f-pc-course").append("<option value='none' selected disabled hidden>Select course</option>");
        $.getJSON(`?handler=Courses&programId=${programId}`, (data) => {
            $.each(data, function (i, item) {
                $("#f-pc-course").append(`<option value="${item.id}">${item.name}</option>`);
            });
        });
    });

    // find and populate electives depending on whichever school was selected
    $('#f-e-school').change(function () {
        $('#f-e-course').prop('disabled', false);

        $("#f-e-course").empty();
        $("#f-e-course").append("<option value='none' selected disabled hidden>Select elective</option>");
        $.getJSON(`?handler=Electives`, (data) => {
            $.each(data, function (i, item) {
                $("#f-e-course").append(`<option value="${item.id}">${item.name}</option>`);
            });
        });
    });
});