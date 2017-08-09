$(document).ready(function () {

    // Check for window size and collapse chapters if too small.
    var sideBar = $("#sideBar");
    onResize();
    $(window).resize(onResize);
    function onResize() {
        // windowSize will be true when window size is more than min-width.
        var windowSize = Modernizr.mq('(min-width:900px)');
        if (windowSize) {
            if (!sideBar.hasClass("in")) sideBar.addClass("in");
        }
        else {
            if (sideBar.hasClass("in")) sideBar.removeClass("in");
        }
    }



    // Initialize popovers.
    function initializePopover(popoverLinks) {

        popoverLinks.popover({ html: "true", trigger: "hover", placement: "bottom" });
        popoverLinks.attr({
            "data-toggle": "popover", href: "#", role: "button"
        })        
    }

    // Get Main Article and List of Related Articles
    // Ajax request to two different methods depending upon which link is clicked.
    // Ajax call to get Article for view.
    // getArticle method returns a partical view which is html data type.
    function getArticle() {

        var theArticle = $(this);
        var article_id = theArticle.attr("id");
        var idString = "#tab" + article_id;
        var tabLink = '<li role="presentation" class="active my-tab-link"><a href="#tab' + article_id + '" aria-controls="home" role="tab" data-toggle="tab">' + article_id + '</a></li>';
        var myTabLink = $("li.my-tab-link");

        if (theArticle.hasClass("main-article")) {
            $.ajax({ url: "Home/GetArticleWithLinks", data: { articleId: article_id }, dataType: "html" }).done(function (msg) {
                // Remove tabs and add new tab
                myTabLink.remove();
                $("ul.nav-tabs").append(tabLink);

                var msgObject = $(msg);
                var mainArticle = msgObject.filter(".my-tab-pane");
                var linkedArticles = msgObject.filter(".linked-article");

                // Remove panes and add new pane.
                $("div.my-tab-pane").remove();
                $("div.tab-content").append(mainArticle);

                // Remove RelatedArticleView and add new view
                $("a.linked-article").remove();
                $('div[name="RelatedArticleView"]').append(linkedArticles);                

                // Initialize popover after every ajax request.
                var popoverLinks = $(idString).find("a.link");
                initializePopover(popoverLinks);
            });
        }
        // class .linked-articles contains all article links which are not main-article.
        // Fetch only main article and add tab/panes to the existing view.
        // Do not replace. Append.
        else {
            // Remove .active from appended main article tab link and pane
            var inactiveTabLink = $(tabLink).removeClass("active");

            // Append tab
            $("ul.nav-tabs").append(inactiveTabLink);

            //Append pane
            $.ajax({ url: "Home/GetArticle", data: { articleId: article_id }, dataType: "html" }).done(function (msg) {
                var inactiveMsg = $(msg).removeClass("active");
                $("div.tab-content").append(inactiveMsg);
                
                // Initialize popover after every ajax request.
                var popoverLinks = $(idString).find("a.link");
                initializePopover(popoverLinks);

                // Show the current tab.
                var hrefString = "a[href='" + idString + "']";
                $(hrefString).tab("show");
            });
        }
    }

    function showTab(e) {
        e.preventDefault();
        $(this).tab('show');       
    }

    function beginEditMode()
    {
        // Append Chapter link to chapter list
        var chapterLink = '<a class="list-group-item list-group-item-danger"  data-toggle="collapse" href="">Add Chapter</a>'
        $(chapterLink).appendTo("#sideBar");

        // Append Section link to section list
        var SectionLink = '<a class="list-group-item list-group-item-danger"  data-toggle="collapse" href="">Add Section</a>'
        $(SectionLink).appendTo('div[id*="Chapter"]');

        // Append Article link to article list
        var articleLink = '<a class="list-group-item list-group-item-danger"  data-toggle="collapse" href="">Add Article</a>'
        $(articleLink).appendTo('div[id*="Section"]');

        

        $(this).prop("disabled", "true");
        $("#mainViewButton").removeAttr("disabled");
        //if (bookView.prop("hidden")) {
        //    bookView.removeAttr("hidden");
        //    console.log("showing book view", bookView.prop("hidden"));
        //}
        //else {
        //    bookView.prop("hidden", "false");
        //    console.log("hiding book view", bookView.prop("hidden"));
        //}        
    }

    function beginViewMode()
    {
        $(this).prop("disabled", "true");
        $("#mainEditButton").removeAttr("disabled");
    }

    initializePopover($("a.link"));


    // One Chapter shown toggles other chapters hidden.
    // Select all div elements having id="Chapter"+ one number.
    var chapterLinks = $("div[id*='Chapter']");
    chapterLinks.on("show.bs.collapse", function () {
        chapterLinks.not($(this)).collapse("hide");
    });

    // One Section shown toggles other sections hidden.
    // Select all div elements having id="Section"+Number.
    var sectionLinks = $("div[id*='Section']");
    sectionLinks.on("show.bs.collapse", function () {
        sectionLinks.not($(this)).collapse("hide");
    });

    // Attach event to BookView.
    $("#sideBar").on("click", "a.main-article", getArticle);

    // Attach Event to MainArticleView
    $('div[name="MainArticleView"]').on("click", "a.link", getArticle);

    // Attach Event to RelatedArticleView
    $('div[name="RelatedArticleView"]').on("click", "a.linked-article", getArticle);

    // Attach Event to Tab links
    $("ul.nav-tabs").on("click", "a", showTab);

    $("#comment").on("keyup", function (event) {
        console.log(event.key);
    });

    $("#mainEditButton").click(beginEditMode);
    $("#mainViewButton").click(beginViewMode);





});





