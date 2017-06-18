// Write your Javascript code.

var app = app || {};

app.imagePreview = (function () {
    'use strict';
    var uploadElement, showPicture;

    var init = function () {
        uploadElement = $("#take-picture")
        showPicture = $(".js-show-picture");

        handlePhotUpload();
        hideDialogHandler();
    };

    var handlePhotUpload = function () {

        uploadElement.on('change', function (event) {
            // Prevent default dragging of selected content
            event.preventDefault();

            var files = event.target.files,
                file;

            if (files && files.length > 0) {
                file = files[0];
                var fileReader = new FileReader();
                fileReader.onload = function (event) {
                    showPicture.attr('src', event.target.result);
                };
                fileReader.readAsDataURL(file);
            }
        });
    }

    var hideDialogHandler = function ($event) {

        $(".js-close-preview").on('click', function ($event) {

            $event.preventDefault();
            clearFileInput(uploadElement);
            showPicture.attr('src', '');
        });
    };

    var clearFileInput = function ($input) {
        if ($input.val() == '') {
            return;
        }
        // Fix for IE ver < 11, that does not clear file inputs
        // Requires a sequence of steps to prevent IE crashing but
        // still allow clearing of the file input.
        if (/MSIE/.test(navigator.userAgent)) {
            var $frm1 = $input.closest('form');
            if ($frm1.length) { // check if the input is already wrapped in a form
                $input.wrap('<form>');
                var $frm2 = $input.closest('form'), // the wrapper form
                    $tmpEl = $(document.createElement('div')); // a temporary placeholder element
                $frm2.before($tmpEl).after($frm1).trigger('reset');
                $input.unwrap().appendTo($tmpEl).unwrap();
            } else { // no parent form exists - just wrap a form element
                $input.wrap('<form>').closest('form').trigger('reset').unwrap();
            }
        } else { // normal reset behavior for other sane browsers
            $input.val('');
        }
    };

    return {
        link: init
    };
})();

$(document).ready(app.imagePreview.link);

//Toggle the hamburger icon
(function () {
    $(".hamburger--arrow").click(function () {
        $(".hamburger--arrow").toggleClass("is-active");
    });
})();
