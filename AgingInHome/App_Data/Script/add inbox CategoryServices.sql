USE [AgingInHome]
GO
/****** Object:  Table [dbo].[CategoryServices]    Script Date: 6/11/2016 1:15:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CategoryServices](
	[CategoryServiceId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](150) NULL,
	[ProviderCategoryId] [int] NULL,
 CONSTRAINT [PK_CategoryServices] PRIMARY KEY CLUSTERED 
(
	[CategoryServiceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[CategoryServices] ON 

GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (1, N'Health Care and Medical Services', 1)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (2, N'Nutritional Care', 1)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (3, N'Social Support', 1)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (4, N'Daily Living Assistance', 1)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (5, N'Nursing Home', 1)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (6, N'Transitional Care', 1)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (7, N'Wheel Chair Transportation', 2)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (8, N'Emergency Services', 2)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (9, N'Urgent Care', 2)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (10, N'Preventative Care', 2)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (11, N'Hospital Transportation', 2)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (12, N'Basic Life Support', 2)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (13, N'Advanced Life Support', 2)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (14, N'Specialty Care Transport', 2)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (15, N'Scheduled Transportations', 2)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (16, N'Social Services', 3)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (17, N'Nutritional Services', 3)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (18, N'Support for Caregivers', 3)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (19, N'Care Management Services', 3)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (20, N'Personal Space', 4)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (21, N'Community Space', 4)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (22, N'Health Care and Medical Services', 4)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (23, N'Emergency Call Systems', 4)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (24, N'Meal Service', 4)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (25, N'Transportation Services', 4)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (26, N'Wellness Programs', 4)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (27, N'Room and Board', 4)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (28, N'Dental Exams', 5)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (29, N'Cleaning', 5)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (30, N'Dental Fillings and Capping', 5)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (31, N'Extractions', 5)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (32, N'Dental Fabrication', 5)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (33, N'Estate Planning', 6)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (34, N'Medicaid Registration Assistance', 6)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (35, N'Medicare Registration Assistance', 6)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (36, N'Disability Claims Assistance', 6)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (37, N'Guardianship', 6)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (38, N'Medical Alert Systems', 7)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (39, N'Fall-Detector Systems', 7)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (40, N'Emergency Responder Contact', 7)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (41, N'Budgeting Services', 8)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (42, N'Credit Counseling', 8)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (43, N'Investment Management', 8)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (44, N'Insurance Assistance', 8)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (45, N'Furniture Transportation', 9)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (46, N'Furniture Packing', 9)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (47, N'Storage', 9)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (48, N'Fragile Furniture Moving', 9)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (49, N'Unloading and Unpacking', 9)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (50, N'Flood Plan Adjustments', 9)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (51, N'Furniture Disposal or Removal', 9)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (52, N'Care-Planning Assessments', 10)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (53, N'Screening for In-Home Services', 10)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (54, N'Legal Reviews', 10)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (55, N'Medical Reviews', 10)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (56, N'Financial Reviews', 10)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (57, N'Crisis Prevention', 10)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (58, N'Client and Family Education', 10)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (59, N'Order Placement and Delivery', 11)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (60, N'Grocery Unpacking', 11)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (61, N'Prepared Meals', 11)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (62, N'Nutrition Education', 11)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (63, N'Accessibility Assessments', 12)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (64, N'Floor Plan Consulting', 12)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (65, N'Remodeling Consultant', 12)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (66, N'Home Companionship', 13)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (67, N'Dressing Assistance', 13)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (68, N'Grooming and Hygiene Assistance', 13)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (69, N'Meal Preparation', 13)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (70, N'Housekeeping', 13)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (71, N'Medication Reminders and Assistance', 14)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (72, N'In-Home Nursing', 14)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (73, N'Injury or Surgery Rehabilitation', 14)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (74, N'Mental Health Counseling', 14)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (75, N'Pain Management', 14)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (76, N'Registered Nurses', 14)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (77, N'Licensed Practical Nurses', 14)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (78, N'Physical Therapists', 14)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (79, N'Occupational Therapist', 14)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (80, N'Home Health Aides', 14)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (81, N'Certified Nursing Assistants', 14)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (82, N'Wound Care', 14)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (83, N'Patient and Caregiver Education', 14)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (84, N'Nutrition Therapy', 14)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (85, N'Intravenous Therapy', 14)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (86, N'Injections', 14)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (87, N'Health Monitoriing', 14)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (88, N'Light Housekeeping', 15)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (89, N'Kitchen and Bathroom Cleaning', 15)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (90, N'Home Organization', 15)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (91, N'Laundry and Dry-Cleaning Services', 15)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (92, N'Long-Term Care Insurance', 16)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (93, N'Health Insurance', 16)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (94, N'Dental Insurance', 16)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (95, N'Vision Insurance', 16)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (96, N'Life Insurance', 16)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (97, N'Bathroom Remodeling', 17)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (98, N'Kitchen Remodeling', 17)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (99, N'Design and Floor Plan Modifications', 17)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (100, N'Lighting Enhancements', 17)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (101, N'Ramp Construction', 17)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (102, N'Stairlift Installation', 17)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (103, N'Move Planning', 18)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (104, N'Organizing and Sorting', 18)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (105, N'Floor Plan Customization', 18)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (106, N'Managing Movers', 18)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (107, N'Shipping and Storage Management', 18)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (108, N'Unpacking and New-Home Setup', 18)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (109, N'Cleaning and Waste Removal', 18)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (110, N'Bill Paying', 19)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (111, N'Financial Records', 19)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (112, N'Budget Preparation', 19)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (113, N'Checkbook Balancing', 19)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (114, N'Negotiation with Creditors', 19)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (115, N'Hand Therapy ', 20)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (116, N'Activities', 20)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (117, N'Injury Recovery', 20)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (118, N'In-Home Hazard Assessment', 20)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (119, N'Home Modifications', 20)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (120, N'Wheel Chair Selection', 20)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (121, N'Driver Rehabilitation', 20)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (122, N'Equipment Recommendations', 20)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (123, N'Eye Exams', 21)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (124, N'Glasses Fittings', 21)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (125, N'Glaucoma Testing', 21)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (126, N'Emergency Treatment', 21)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (127, N'Management of Eye Conditions', 21)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (128, N'Age-Related Macular Degeneration', 21)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (129, N'Cataracts Treatment', 21)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (130, N'Diabetic Retinopathy', 21)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (131, N'Dry Eye Treatment', 21)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (132, N'Prescription and Medication Delivery', 22)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (133, N'In-Home Medical Evaluations', 22)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (134, N'Medication Organization', 22)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (135, N'Compounding Medications', 22)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (136, N'Information for Caregivers', 22)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (137, N'Recommendations to Prescribers', 22)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (138, N'Medication Distribution Services', 22)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (139, N'Pain Management Counseling', 22)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (140, N'Intravenous Therapy', 22)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (141, N'Nutrition Assessment', 22)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (142, N'Acute and Chronic Pain', 23)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (143, N'Aquatic Therapy', 23)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (144, N'Back and Neck Pain', 23)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (145, N'Concussion Management', 23)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (146, N'Dry Needling', 23)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (147, N'Fibromyalgia', 23)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (148, N'Foot and Ankle Pain', 23)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (149, N'General Orthopedics', 23)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (150, N'Graston Technique', 23)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (151, N'Injury Screenings', 23)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (152, N'Kinesio Taping', 23)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (153, N'Leg and Knee Pain', 23)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (154, N'Lymphedema', 23)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (155, N'Manual Therapy', 23)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (156, N'Occupational Therapy/Hand Therapy', 23)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (157, N'Pre and Post-Operative Care', 23)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (158, N'Shoulder Pain', 23)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (159, N'Spine Rehab', 23)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (160, N'Sports Injuries', 23)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (161, N'Total Joint Rehab', 23)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (162, N'Vestibular Therapy', 23)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (163, N'Wellness Programs', 23)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (164, N'Work Injury Rehabilitation ', 23)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (165, N'General Podiatry', 24)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (166, N'Orthopedic Services', 24)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (167, N'High-Risk Wound Care', 24)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (168, N'Neuro-Podiatrist', 24)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (169, N'Podiatric Oncologist', 24)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (170, N'Circulation Specialist', 24)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (171, N'Skin Podiatry', 24)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (172, N'Diabetic Foot Care', 24)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (173, N'Chronic Disease Management', 25)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (174, N'Medication Assistance and Prescriptions', 25)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (175, N'Checkups and Exams', 25)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (176, N'Complete Physicals', 25)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (177, N'Wellness Checks', 25)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (178, N'Acute Sick Visit', 25)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (179, N'Manage Chronic Health Conditions', 25)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (180, N'Medication Monitoring', 25)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (181, N'Routine Tests & Monitoring', 25)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (182, N'Immunizations', 25)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (183, N'Specialist Referrals ', 25)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (184, N'Home Appraisals', 26)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (185, N'Financial Assistance Guidance', 26)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (186, N'Senior Real Estate Specialist (SRES-Certified)', 26)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (187, N'Reverse Mortgage Counselor', 26)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (188, N'Pre-Listing Home Preparation', 26)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (189, N'Home Sales', 26)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (190, N'Service-Needs Assessment', 27)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (191, N'Health and Wellness Facilitation', 27)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (192, N'Care-Giver Management', 27)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (193, N'Community Resource Assistance', 27)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (194, N'Cognition Enhancement', 28)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (195, N'Picture-Word Association', 28)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (196, N'Vocal Volume Enhancement', 28)
GO
INSERT [dbo].[CategoryServices] ([CategoryServiceId], [Name], [ProviderCategoryId]) VALUES (197, N' Post-Stroke Speech Therapy', 28)
GO
SET IDENTITY_INSERT [dbo].[CategoryServices] OFF
GO
ALTER TABLE [dbo].[CategoryServices]  WITH CHECK ADD  CONSTRAINT [FK_CategoryServices_ProviderCategory] FOREIGN KEY([ProviderCategoryId])
REFERENCES [dbo].[ProviderCategory] ([Id])
GO
ALTER TABLE [dbo].[CategoryServices] CHECK CONSTRAINT [FK_CategoryServices_ProviderCategory]
GO
