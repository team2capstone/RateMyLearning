jQuery(document).ready(function ($) {
    // Edit Profile
    $("#editProfile").click(function () {
        $("#saveProfileButton").append(
            '<div class="col" id="saveProfile"><button class="btn btn-info my-4 btn-block" type="submit" id="saveProfileChanges">Save</button></div>');
        $(this).prop("disabled", true).css("opacity", 0.3);
        $("#editProfileFormEmail").prop("disabled", false);
        $("#editProfileFormStudentNumber").prop("disabled", false);
        $("#editProfileFormFacultyNumber").prop("disabled", false);
    });

    // Save Profile
    $("#saveProfileChanges").click(function () {
        $("#saveProfileButton").empty();
        $("#saveProfileButton").prop("disabled", false);
        $("#editProfileFormEmail").prop("disabled", true);
        $("#editProfileFormStudentNumber").prop("disabled", true);
        $("#editProfileFormFacultyNumber").prop("disabled", true);
    });
});