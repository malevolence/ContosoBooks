(function (window, undefined) {
    'use strict';

    // avoid console errors in browsers that lack a console.
    var method;
    var noop = function () { };
    var methods = [
        'assert', 'clear', 'count', 'debug', 'dir', 'dirxml', 'error',
        'exception', 'group', 'groupCollapsed', 'groupEnd', 'info', 'log',
        'markTimeline', 'profile', 'profileEnd', 'table', 'time', 'timeEnd',
        'timeline', 'timelineEnd', 'timeStamp', 'trace', 'warn'
    ];
    var length = methods.length;
    var console = (window.console = window.console || {});

    while (length--) {
        method = methods[length];

        // Only stub undefined methods.
        if (!console[method]) {
            console[method] = noop;
        }
    }

    var BW = window.BW = window.BW || {};

    BW.formUtils = {};

    BW.formUtils.hijaxForm = function (selector, cb) {
        var form$ = $(selector);

        form$.on('submit', function (e) {
            e.preventDefault();

            var url = form$.attr('action');
            var data = form$.serialize();

            $.post(url, data, cb);
        });
    };

    PH.formUtils.showAlert = function (opts) {
        var defaults = {
            css: 'alert-info',
            msg: 'Default Message'
        };

        var opts = $.extend({}, defaults, opts);

        $('<div></div>', {
            'class': 'fade in alert ' + opts.css,
            html: opts.msg
        }).append($('<button />', {
            html: '&times;',
            'type': 'button',
            'class': 'close',
            'data-dismiss': 'alert'
        })).insertBefore('#main-row');
    };

})(window);