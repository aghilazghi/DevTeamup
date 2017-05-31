
function Discussion(data) {

    var self = this;

    data = data || {};

    self.id = data.id;
    self.teamupId = data.teamupId;
    self.body = ko.observable(data.body || "");
    self.postedBy = data.postedByName || "";
    self.postedById = ko.observable(data.postedById || "");
    self.createdOn = moment(data.createdOn).fromNow();
    self.replies = ko.observableArray();
    self.newReply = ko.observable();

    self.addReply = function() {
        var reply = new Reply();
        reply.discussionId = self.id;
        reply.body(self.newReply());
        return $.ajax({
                url: "/api/replies/",
                dataType: "json",
                contentType: "application/json",
                cache: false,
                type: "POST",
                data: ko.toJSON(reply)
            })
            .done(function(result) {
                self.replies.push(new Reply(result));
                self.newReply("");
                toastr.success("Reply added successfully!");
            })
            .fail(function() {
                toastr.error("Unable to add your reply!");
            });
    }

    if (data.replies) {
        var mappedReplies = $.map(data.replies,
            function(item) {
                return new Reply(item);
            });

        self.replies(mappedReplies);
    }
}

function Reply(data) {
    var self = this;
    data = data || {};

    self.id = data.id;
    self.discussionId = data.discussionId;
    self.body = ko.observable(data.body || "");
    self.repliedBy = data.repliedByName || "";
    self.repliedById = ko.observable(data.repliedById || "");
    self.replyCreatedOn = moment(data.createdOn).fromNow();
}

function ViewModel(data) {
    var self = this;

    var teamupId = data;
    self.discussions = ko.observableArray();
    self.newDiscussion = ko.observable();

    self.loadDiscussions = function() {
        $.ajax({
                url: "/api/discussions/" + teamupId,
                dataType: "json",
                contentType: "application/json",
                cache: false,
                type: "GET"
            })
            .done(function(data) {
                var mappedDiscussions = $.map(data, function(item) { return new Discussion(item) });
                self.discussions(mappedDiscussions);
                toastr.success("Discussion loaded successfully!");
            })
            .fail(function() {
                toastr.error("Unable to load your discussions!");
            });
    }

    self.addDiscussion = function () {

        var discussion = new Discussion();
        discussion.teamupId = teamupId;
        discussion.body(self.newDiscussion());

        return $.ajax({
                url: "/api/discussions/",
                dataType: "json",
                contentType: "application/json",
                cache: false,
                type: "POST",
                data: ko.toJSON(discussion)
            })
            .done(function(result) {
                self.discussions.splice(0, 0, new Discussion(result));
                self.newDiscussion("");
                toastr.success("Discussion is created successfully!");
            })
            .fail(function() {
                toastr.error("Unable to add your discussion!");
            });
    }

    self.loadDiscussions();
    return self;
}