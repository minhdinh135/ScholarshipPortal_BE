-- Insert into scholarship_programs
INSERT INTO scholarship_programs
VALUES
(18, 'PhD Research Excellence Award', 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTQmkvlHDU2Ut2U_rlwukwpHtnQCF0f2b_1bw&s', 
 'Supporting PhD students with outstanding research proposals.', '25000.00', 3, 
 '2024-11-09 00:00:00.000000', 'FINISHED', 2, 1, 1, 1, 
 '2024-11-24 17:35:03.423805', '2024-11-24 17:41:09.742665'),
(19, 'Community Leadership Scholarship', 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTQmkvlHDU2Ut2U_rlwukwpHtnQCF0f2b_1bw&s', 
 'Awarded to students showing significant community impact.', '7000.00', 3, 
 '2024-11-09 00:00:00.000000', 'FINISHED', 2, 1, 1, 1, 
 '2024-11-24 17:51:58.381189', '2024-11-24 17:57:06.234577'),
(1, 'Global Excellence Scholarship', 'https://example.com/images/global_excellence.jpg', 
 'Awarded to outstanding students demonstrating academic excellence and leadership potential.', '10000.00', 5, 
 '2024-12-15 23:59:59.000000', 'Open', 1, 2, 3, 5, 
 '2024-11-23 15:09:38.822461', '2024-11-23 15:09:38.822460'),
(2, 'Future Innovators Fellowship', 'https://example.com/images/future_innovators.jpg', 
 'Supporting aspiring scientists and engineers working on innovative projects.', '15000.00', 3, 
 '2025-01-20 23:59:59.000000', 'Open', 2, 1, 4, 2, 
 '2024-11-23 15:10:04.211540', '2024-11-23 15:10:04.211539'),
(3, 'Global Women in STEM Award', 'https://example.com/images/women_in_stem.jpg', 
 'Encouraging women to pursue careers in STEM fields through financial aid and mentorship.', '12000.00', 10, 
 '2024-11-30 23:59:59.000000', 'Open', 3, 3, 2, 7, 
 '2024-11-23 15:10:25.404868', '2024-11-23 15:10:25.404867'),
(13, 'Global Change-Maker Scholarship', 'https://example.com/images/change_maker.jpg', 
 'Recognizing students with initiatives addressing global challenges.', '10000.00', 8, 
 '2024-12-25 23:59:59.000000', 'Open', 6, 2, 5, 8, 
 '2024-11-23 15:13:45.727921', '2024-11-23 15:13:45.727920');


-- Insert into scholarship_program_certificates
INSERT INTO scholarship_program_certificates
VALUES
(18, 1),
(19, 1),
(1, 2),
(13, 2),
(2, 3),
(3, 4),
(3, 5);


-- Insert into award_milestones
INSERT INTO award_milestones
VALUES
(6, '2024-11-11 17:35:00.000000', '2025-11-30 17:35:00.000000', '20000.00', 18, '2024-11-24 17:35:57.652140', '2024-11-24 17:35:57.652139'),
(7, '2025-12-01 17:36:00.000000', '2026-12-03 17:36:00.000000', '2500.00', 18, '2024-11-24 17:36:25.716987', '2024-11-24 17:36:25.716986'),
(8, '2026-12-08 17:36:00.000000', '2027-12-11 17:36:00.000000', '2500.00', 18, '2024-11-24 17:37:21.984472', '2024-11-24 17:37:21.984471'),
(9, '2023-11-27 17:52:00.000000', '2024-11-22 17:52:00.000000', '3000.00', 19, '2024-11-24 17:52:26.610120', '2024-11-24 17:52:26.610119'),
(10, '2024-11-23 19:07:00.000000', '2025-12-03 17:52:00.000000', '3000.00', 19, '2024-11-24 17:52:40.770368', '2024-11-24 17:52:40.770367'),
(11, '2025-12-08 17:52:00.000000', '2026-12-10 17:52:00.000000', '1000.00', 19, '2024-11-24 17:52:56.586618', '2024-11-24 17:52:56.586617'),
(14, '2024-12-27 13:53:00.000000', '2024-12-29 13:54:00.000000', '5000.00', 1, '2024-11-25 13:54:23.909117', '2024-11-25 13:54:23.909092'),
(15, '2025-01-02 13:54:00.000000', '2025-01-25 13:54:00.000000', '5000.00', 1, '2024-11-25 13:54:48.115586', '2024-11-25 13:54:48.115584'),
(16, '2025-01-25 13:55:00.000000', '2025-01-28 13:56:00.000000', '10000.00', 2, '2024-11-25 13:56:13.973473', '2024-11-25 13:56:13.973472'),
(17, '2025-01-30 13:56:00.000000', '2025-02-08 13:56:00.000000', '5000.00', 2, '2024-11-25 13:56:31.854299', '2024-11-25 13:56:31.854298'),
(18, '2024-12-01 13:56:00.000000', '2024-12-10 13:57:00.000000', '10000.00', 3, '2024-11-25 13:57:12.780165', '2024-11-25 13:57:12.780164'),
(19, '2024-12-11 13:57:00.000000', '2024-12-20 13:57:00.000000', '2000.00', 3, '2024-11-25 13:57:31.138455', '2024-11-25 13:57:31.138455'),
(20, '2025-01-01 13:58:00.000000', '2025-01-10 13:58:00.000000', '5000.00', 13, '2024-11-25 13:58:22.900649', '2024-11-25 13:58:22.900648'),
(21, '2025-01-18 13:58:00.000000', '2025-01-25 13:58:00.000000', '5000.00', 13, '2024-11-25 13:58:42.157973', '2024-11-25 13:58:42.157972');


-- Insert into applications
INSERT INTO applications
VALUES
(14, '2024-11-24 17:37:54.664341', 'Approved', 14, 18, '2024-11-24 17:37:54.664760', '2024-11-24 14:09:29.457039'),
(15, '2024-11-24 17:38:42.604807', 'Approved', 13, 18, '2024-11-24 17:38:42.605039', '2024-12-12 10:36:00.000000'),
(16, '2024-11-24 17:40:52.816397', 'Approved', 12, 18, '2024-11-24 17:40:52.816823', '2024-11-24 17:41:09.148999'),
(17, '2024-11-27 17:55:25.506891', 'NeedExtend', 13, 19, '2024-11-24 17:55:25.507166', '2024-11-24 14:17:22.425610'),
(18, '2024-11-27 17:56:16.570925', 'NeedExtend', 14, 19, '2024-11-24 17:56:16.571198', '2024-11-24 14:15:24.414399'),
(19, '2024-11-27 17:56:51.853728', 'NeedExtend', 12, 19, '2024-11-24 17:56:51.853966', '2024-11-24 18:23:39.969624');

-- Insert into application_documents
INSERT INTO application_documents
VALUES
(13, 'CV', 'Research Proposal', 'https://res.cloudinary.com/djiztef3a/raw/upload/v1732444673/rxfpb53czhsxiygsuvwx.pdf', 14, '2024-11-24 17:37:54.664760', '2024-11-24 17:37:54.664760'),
(14, 'Schôl', 'Portfolio', 'https://res.cloudinary.com/djiztef3a/raw/upload/v1732444721/lg4gpjrejwanbcxiqhww.pdf', 15, '2024-11-24 17:38:42.605040', '2024-11-24 17:38:42.605039'),
(15, 'Oliver', 'Personal Statement', 'https://res.cloudinary.com/djiztef3a/raw/upload/v1732444851/wgc2pcyhgctynhjpysww.pdf', 16, '2024-11-24 17:40:52.816823', '2024-11-24 17:40:52.816823'),
(17, 'grace', 'CV/Resume', 'https://res.cloudinary.com/djiztef3a/raw/upload/v1732445775/iple0mwdsoakdzlqoq93.pdf', 18, '2024-11-24 17:56:16.571198', '2024-11-24 17:56:16.571198'),
(18, 'oliver', 'CV/Resume', 'https://res.cloudinary.com/djiztef3a/raw/upload/v1732445810/aqxoszew6c5nvyuad7r3.pdf', 19, '2024-11-24 17:56:51.853966', '2024-11-24 17:56:51.853966'),
(22, 'emma', 'Academic Transcript', 'https://res.cloudinary.com/djiztef3a/raw/upload/v1732447467/kakxnfdvvelolgnmuv8i.pdf', 17, '2024-11-24 18:24:28.466817', '2024-11-24 18:24:28.466816');

-- Insert into wallets
INSERT INTO wallets
VALUES
(1, 'Lucas Nguyen', '42424242424242', '0.00', 1, '2024-11-24 05:29:13.183698', '2024-11-24 05:29:13.183694'),
(2, 'Mason Ho', '424242424242', '0.00', 2, '2024-11-24 05:33:26.350322', '2024-11-24 05:33:26.350320'),
(3, 'Grace Nguyen', '424242424242', '0.00', 3, '2024-11-24 05:53:30.723703', '2024-11-24 14:09:28.990594'),
(4, 'Oliver', '4242424242', '0.00', 4, '2024-11-24 17:45:33.177188', '2024-11-24 17:45:33.177188'),
(5, 'Emma', '4242424242', '0.00', 5, '2024-11-24 17:46:13.332058', '2024-11-24 17:46:13.332058'),
(6, 'Ethan', '4242424242', '0.00', 6, '2024-11-24 17:46:59.248243', '2024-11-24 17:46:59.248241'),
(7, 'Lucas Nguyen', '42424242424242', '0.00', 7, '2024-11-24 05:29:13.183698', '2024-11-24 05:29:13.183694'),
(8, 'Mason Ho', '424242424242', '0.00', 8, '2024-11-24 05:33:26.350322', '2024-11-24 05:33:26.350320'),
(9, 'Grace Nguyen', '424242424242', '0.00', 9, '2024-11-24 05:53:30.723703', '2024-11-24 14:09:28.990594'),
(10, 'Oliver', '4242424242', '0.00', 10, '2024-11-24 17:45:33.177188', '2024-11-24 17:45:33.177188'),
(11, 'Emma', '4242424242', '0.00', 11, '2024-11-24 17:46:13.332058', '2024-11-24 17:46:13.332058'),
(12, 'Ethan', '4242424242', '0.00', 12, '2024-11-24 17:46:59.248243', '2024-11-24 17:46:59.248241'),
(13, 'Lucas Nguyen', '42424242424242', '0.00', 13, '2024-11-24 05:29:13.183698', '2024-11-24 05:29:13.183694'),
(14, 'Mason Ho', '424242424242', '0.00', 14, '2024-11-24 05:33:26.350322', '2024-11-24 05:33:26.350320');

-- Insert into services
INSERT INTO services
VALUES
(1, 'CV Polish for Competitive Scholarships', 'Ensure your CV meets the highest standards for competitive scholarship programs.', 'CV_REVIEW', '65.00', 'Active', 8, '2024-11-24 05:29:31.603681', '2024-11-24 05:29:31.603680'),
(2, 'Scholarship Document Translation', 'Professional translation for scholarship-related documents.', 'TRANSLATION', '85.00', 'Active', 8, '2024-11-24 05:30:47.466831', '2024-11-24 05:30:47.466828'),
(3, 'Scholarship Essay Feedback', 'Detailed critique and enhancement of your scholarship essay drafts.', 'ESSAY_WRITING', '85.00', 'Active', 8, '2024-11-24 05:31:12.696851', '2024-11-24 05:31:12.696850'),
(4, 'Comprehensive Application Review', 'Full evaluation of your scholarship application with actionable improvements.', 'APPLICATION_REVIEW', '85.00', 'Active', 8, '2024-11-24 05:31:38.193868', '2024-11-24 05:31:38.193867'),
(5, 'Virtual Interview Preparation', 'Practice and tips for acing virtual scholarship interviews.', 'INTERVIEW_COACHING', '85.00', 'Active', 8, '2024-11-24 05:32:00.604780', '2024-11-24 05:32:00.604778'),
(6, 'Custom Recommendation Letter Draft', 'Assistance in drafting personalized recommendation letters.', 'RECOMMENDATION_LETTER', '85.00', 'Active', 8, '2024-11-24 05:32:24.914395', '2024-11-24 05:32:24.914394'),
(7, 'Scholarship Finder Tool Access', 'Access to a curated database of scholarships tailored to your profile.', 'SCHOLARSHIP_SEARCH', '45.00', 'Active', 10, '2024-11-24 05:35:25.231270', '2024-11-24 05:35:25.231269'),
(8, 'Scholarship Finder Tool Access', 'Access to a curated database of scholarships tailored to your profile.', 'SCHOLARSHIP_SEARCH', '45.00', 'Active', 10, '2024-11-24 05:36:22.465812', '2024-11-24 05:36:22.465809'),
(9, 'CV Tailoring for Scholarships', 'Personalized CV review and enhancement to highlight scholarship-specific achievements.', 'CV_REVIEW', '45.00', 'Active', 10, '2024-11-24 05:36:55.127676', '2024-11-24 05:36:55.127675'),
(10, 'Academic Translation Services', 'Translation of academic documents to the required language for scholarship applications.', 'TRANSLATION', '45.00', 'Active', 10, '2024-11-24 05:37:16.387580', '2024-11-24 05:37:16.387579'),
(11, 'Winning Scholarship Essay Guide', 'Professional assistance in crafting compelling scholarship essays.', 'ESSAY_WRITING', '45.00', 'Active', 10, '2024-11-24 05:37:39.294335', '2024-11-24 05:37:39.294334'),
(12, 'Scholarship Application Critique', 'Detailed feedback on your entire scholarship application to ensure success.', 'APPLICATION_REVIEW', '45.00', 'Active', 10, '2024-11-24 05:38:01.112041', '2024-11-24 05:38:01.112040');