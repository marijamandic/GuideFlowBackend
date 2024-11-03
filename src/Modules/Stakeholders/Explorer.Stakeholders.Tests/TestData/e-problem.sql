INSERT INTO stakeholders."Problems"(
	"Id", "UserId", "TourId", "Details", "Resolution")
	VALUES (-1, 1, 2, json_build_object('Category', 0, 'Priority', 0, 'Description', 'pa sta znam nije lose'), json_build_object('ReportedAt', NOW(), 'IsResolved', TRUE, 'Deadline', NOW() + INTERVAL '5 days'));
INSERT INTO stakeholders."Problems"(
	"Id", "UserId", "TourId", "Details", "Resolution")
	VALUES (-2, 1, 2, json_build_object('Category', 0, 'Priority', 0, 'Description', 'pa sta znam nije lose'), json_build_object('ReportedAt', NOW(), 'IsResolved', FALSE, 'Deadline', NOW() + INTERVAL '5 days'));
INSERT INTO stakeholders."Problems"(
	"Id", "UserId", "TourId", "Details", "Resolution")
	VALUES (-3, 1, 2, json_build_object('Category', 0, 'Priority', 0, 'Description', 'pa sta znam nije lose'), json_build_object('ReportedAt', NOW(), 'IsResolved', FALSE, 'Deadline', NOW() + INTERVAL '5 days'));