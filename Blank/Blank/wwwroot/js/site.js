(function () {

    
    var ele = $("userName");
    ele.text("David Nemeth");

    var main = $("#main");
    main.on("mouseenter",
        function() {
            main.style = "background-color: #888;";
        });
    main.on("mouseleave",
        function() {
            main.style = "";
        });

    var $sidebarAndWrapper = $("#sidebar,#wrapper");
    $("#sidebarToggle").on("click",
        function() {
            $sidebarAndWrapper.toggleClass("hide-sidebar");
            if ($sidebarAndWrapper.hasClass("hide-sidebar")) {
                $(this).text("Show Sidebar");
            } else {
                $(this).text("Hide Sidebar");
            }

        });
})();