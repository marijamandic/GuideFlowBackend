INSERT INTO stakeholders."Problems"(
	"Id", "UserId", "TourId", "Category", "Priority", "Description", "ReportedAt", "Resolution")
	VALUES (-1, 1, 2, 0, 0, 'pa sta znam nije lose', NOW(), json_build_object('IsResolved', TRUE, 'Deadline', NOW()));
INSERT INTO stakeholders."Problems"(
	"Id", "UserId", "TourId", "Category", "Priority", "Description", "ReportedAt", "Resolution")
	VALUES (-2, 1, 2, 0, 0, 'pa sta znam nije lose', NOW(), json_build_object('IsResolved', FALSE, 'Deadline', NOW()));
INSERT INTO stakeholders."Problems"(
	"Id", "UserId", "TourId", "Category", "Priority", "Description", "ReportedAt", "Resolution")
	VALUES (-3, 1, 2, 0, 0, 'pa sta znam nije lose', NOW(), json_build_object('IsResolved', FALSE, 'Deadline', NOW()));