# FutureGrad
e-learning system with special features including stylus-friendly marking tools

This project was originally written in PHP and was part of my dissertation at Kingston University (September 2016 - May 2017).

Below, is a sample of my dissertation.

# Abstract
This project seeks to improve the quality of e-marking software by addressing issues that lecturers and students face when using current systems such as TurnItIn. A review of relevant pedagogic, e-learning and psychological literature was conducted, along with a state of the art review of software and technologies. It was concluded that the usability of e-marking systems is a key area that is currently being worked on by many e-learning platforms such as Canvas. Following testing, the application provided lecturers with annotation features thus bridging the gap between e-marking and the traditional pen-and-paper system, as well as several automated features that enforce anonymous marking and reduced workload for academics, in the aim to enforce fairness in the education system as well as speeding up the marking process.

# Introduction
Electronic marking has brought many benefits to the e-learning field. The aims of this project are to enhance usability of e-marking systems, and to provide students with more effective feedback. To achieve this, a literature review will be conducted to find out what factors make feedback effective and whether any of these are overlooked by current systems. Interviews will be carried out with students to find out how feedback can be made more meaningful to them as well as any issues they face when completing/submitting assignments. Requirements will be gathered and grouped into functionalities to ease the scheduling process. By the end of this project, it is hoped that a web application would have been developed, that would enforce feedback of a high quality and provide a stylus-friendly e-marking feature.

# Literature Review
In contrast to the pen-and-paper system, e-Marking has allowed universities to track and back-up student submissions, record and analyse student results, as well as allow flexible working for lecturers, without the need of printing and managing paper work. It has also opened doors to many people, in terms of accessibility, allowing those with disabilities to complete courses via the internet.  Electronic marking has also enforced the use of typed submissions, over hand-written submissions, which has been a factor in examiner bias according to research by Markham et al (1976). 

Marker Bias
Examiner bias can be the result of unconscious as well as conscious motives. An example of unconscious bias would be the contrast effect, where perception is affected by comparing experiences to previous ones. Anonymous marking has been introduced as an option in many e-marking systems, to combat examiner bias, which could lead to unreliable marking. For example, Dennis and Newstead (1994) found that the race, gender and age of students in higher education were three of many factors that lead to examiner bias. Similar conclusions were drawn from studies by Filippou et al (2011), which found that perception of intelligence was affected by the physical attractiveness, sense of humour and friendliness of a person, which lead to bias in marking. However, an alternate explanation to these findings would be that people with a good sense of humour tend to be more intelligent, as demonstrated by the findings of Greengross and Miller (2011).

Various studies have supported anonymous marking as a solution to this issue - for example, the research by the Association of the University Teachers demonstrated the number of firsts awarded to female students increased, subsequent to the introduction of anonymous marking. However, it is not clear whether this is a causal link. Although the link may be partly due to unconscious bias (e.g. when a marker’s perception is affected by their expectations) and/or discrimination, it is likely that other extraneous variables are involved. For example, Else-Quest (2010) found that females are more self-conscious than males – it could therefore be interpreted that female students are less likely to take risks in multiple-choice questions, and less likely to express their opinions during essay-based assessments, thus gaining a lower mark. 

Studies carried out at the University of Bath (2014), has shown that anonymous marking improved student performance, as a result of students being more open about their ideas, as well as reducing examiner bias. The same study also found that students were more likely to express concerns about their course and participate in classroom discussions if anonymous marking was in place. This idea is supported by research by Milgram (1963) and Zimbardo (1971), both of which provides evidence for the theory of deindividualisation, where anonymity leads to lowered levels of self-awareness and fear, resulting in changes to behaviour. With regards to anonymous marking, students would not feel that the lecturers’ perception of them would affect the marks that they will be awarded. Therefore, they are more likely to participate in class discussions, openly; anonymous marking does not only enforce fairness in the education system but also increases general student satisfaction through engagement, which enforces immediate and formative feedback.
Despite the benefits of anonymous marking, it is not favoured by many academics. Studies by Ferrel (2014) have identified reasons behind why anonymous marking is not enforced, such as the barriers in managing students with special needs/mitigating circumstances as well as the loss of anonymity due to students having their names on documents rather than their IDs.  Other issues raised include the lack of anonymity during oral presentations, where race has been identified as the major factor in marker bias. Again, this is not a causal link; it is possible that international students lack confidence during oral presentation due to difference in accent or language difficulty, causing them to underperform. In regards to the project, more research is to be done on how lecturers manage students with special needs, and whether any of the responsibilities could be aided by automated features.

Immediate Feedback Vs Delayed Feedback
Studies by Epstein and Lazarus (2002) have found that students tend to show increased performance on immediate feedback than delayed feedback.  However, later studies have supported that immediate feedback was effective for difficult content, but delayed feedback was more effective for easier content. This may be the result of the way in which information is processed by the brain; for an effective learning process, three stages must occur consecutively – attention, rehearsal and retrieval, which was referred to as the Memory Model by Atkinson and Shiffrin (1968). For example, an assessment that was perceived to be “easy” would imply that the earlier stages of learning (i.e. attention) to have been successful. Therefore, delayed feedback may be more beneficial as it enforces rehearsal of the information. In terms of the system being developed, it is possible that students are asked how easy they found the assignment; the information could then be used to prioritise the assignments that require earlier feedback.

Ignored Feedback
According to Duncan (2007), a handful of students ignore feedback provided by the academics; one reason for this is that students do not feel that acknowledging the comments will change the result of their assignment, particularly if the content is not relevant to future topics, within that module. This highlights the importance of active involvement from both parties: the students, as well as the academics. To address this issue, it is important that academics provide feedback during the course of projects, rather than at the end. 
On-Screen Marking

There have been several studies into whether the mode of marking (paper vs on-screen) has an impact on the marker-reliability. Paek (2005) found that reading longer passages on-screen was more difficult, and lowered comprehension; this finding was used to highlight the possible effect on marking on-screen. Although it could be argued that these findings lack historical validity, in that many screens at the time were cathode-ray tube (CRT), more recent studies by Mangen et al (2013) supported the findings. It was suggested that reading from paper increased the likelihood of recalling where the information was located (spatial-temporal memory), and therefore was more likely to be revisited, if necessary. Despite this, many studies, including Johnson (2012), found that the accuracy of marking assessments on-screen was of equal quality, as those marked on paper. A possible explanation to this may be that studies tend to use student participants; results may not apply to academics. An alternative explanation could be that academics have adapted to reading off a screen, which has improved their comprehension over time. However, the study showed that markers faced more difficulties marking on-screen, due to the barriers in annotating documents, as well as navigating through the text. This raises usability concerns of the current systems, which must be considered when developing the proposed system, in the hope that it could speed up the marking process. 
A possible solution to improving usability would be to bridge the gap between on-screen marking and traditional marking. Cognitive researchers, Longchamp and Velay (2014), found that comprehension was improved in children and adults when reading handwritten content than typed content, which provides further support towards implementing such a feature. According to Chang (2012), the quality of feedback was four times better when handwritten, compared to feedback provided electronically. However, accessibility was improved by electronic marking, in that it allowed students to access feedback via their desktop, laptops and mobile devices. An earlier study by Fergeson (2011) found that students appreciated handwritten feedback more due to the personal nature of comments, i.e. feedback was more relevant to the students. This was not the case in e-marking where students are provided with more general feedback. Despite feedback being accessible from mobile devices, Singh (2010) found that comprehension was significantly reduced when reading off a smaller screen due to the reduced number of words users see at a given time, thus placing more demand on users’ memory. It was found that comprehension was reduced with the difficulty of the content. Therefore, the new system should encourage students to view feedback on a screen that is no smaller than a tablet as it is vital that students absorb the feedback provided by their academics.

Current Technologies
TurnItIn includes plagiarism detection features as well as the GradeMark feature that allows academics to annotate students’ work, thus simulating the pen-and-paper system. However, a great deal of training is required to use the system, which can be off-putting for many markers. TurnItIn also provides academics with information on student engagement by gathering data on students who have viewed the feedback that the academics have provided them with. Reliability of the data is increased by recording the time spent by students on viewing feedback. Despite all the available features, TurnItIn does not support group submissions – a problem raised by many students and academics. This means that the team leader submits on behalf of all members; the submission is marked, and results are entered manually by academics; the other members of the team are not informed whether the submission has been made by their leader. One problem with this is that the system would not be able to detect errors; for example, if a member is awarded the mark of a team, that they do not belong to. It is possible that the proposed system could remove the need of entering data for each member, by automation, which could speed up the marking process of group submissions. As mentioned previously, this is important as earlier feedback may be more beneficial to student performance. 

Conclusion
It is clear that there are many ways in which the effectiveness of marking can be improved. Firstly, the level of bias that is present in the marks awarded to students has been emphasised in the studies mentioned above. Previous attempts to solve the issue has been explored, such as anonymous marking, and the reasons why this is not favoured among lecturers. These issues are to be addressed by the new system, possibly by implementing a system that manages students who have special needs. For example, recording whether a student is entitled to extra time could allow for the development of an automated deadline extension. Secondly, the delay in providing feedback has been shown to have an effect on students’ performance. The new system will aim to provide a solution to this, by aiding lecturers in time-management. This could be achieved by enhancing the usability of e-marking systems which would hopefully provide them with more time on marking rather than administrative duties. During the literature review, the academics’ side of marking has been thoroughly explored and some aspects of the student experience was touched upon, where it was discovered that handwritten feedback improved the quality of the feedback by four times. To gain a more detailed view, interviews will be carried out to find out how feedback provided by the academics could be improved from the students’ perspective.

