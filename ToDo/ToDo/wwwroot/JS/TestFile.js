var eventLogger = {
    active: true,
    enabledFunctions: {
        click: true,
        eventHandler: true,
        transition: true
    },

    initialize: function () {
        var self = this;
        $(document).on('click', function (event) {
            self.logClick(event);
        });
        $(document).on('transitionend', function (event) {
            self.logTransition(event);
        });
    },

    logClick: function (event) {
        if (this.active && this.enabledFunctions.click) {
            console.log('Element clicked:', event.target);
        }
    },

    logEventHandler: function () {
        if (this.active && this.enabledFunctions.eventHandler) {
            console.log('Event handler called.');
        }
    },

    logTransition: function (event) {
        if (this.active && this.enabledFunctions.transition) {
            console.log('Transition occurred on element:', event.target);
        }
    },

    disableLogging: function () {
        this.active = false;
    },

    enableLogging: function () {
        this.active = true;
    },

    disableFunction: function (func) {
        if (this.enabledFunctions.hasOwnProperty(func)) {
            this.enabledFunctions[func] = false;
        }
    },

    enableFunction: function (func) {
        if (this.enabledFunctions.hasOwnProperty(func)) {
            this.enabledFunctions[func] = true;
        }
    }
};

