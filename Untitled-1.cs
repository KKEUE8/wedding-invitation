from pptx import Presentation
from pptx.util import Inches, Pt
from pptx.dml.color import RGBColor
from pptx.enum.text import PP_ALIGN, MSO_ANCHOR
from pptx.enum.shapes import MSO_SHAPE
from pathlib import Path

out="/mnt/data/IBM_Bob_Canva_Style_Asset_Reconciliation.pptx"
asset_dir=Path("/mnt/data/bob_assets")
prs=Presentation()
prs.slide_width=Inches(13.333)
prs.slide_height=Inches(7.5)

NAVY=RGBColor(10,27,43); BLUE=RGBColor(15,98,254); CYAN=RGBColor(80,190,255)
WHITE=RGBColor(255,255,255); BG=RGBColor(247,249,252); TEXT=RGBColor(28,38,50)
MUTED=RGBColor(101,114,128); GREEN=RGBColor(24,154,92); RED=RGBColor(211,70,70)
ORANGE=RGBColor(235,137,35); PALE=RGBColor(231,241,255); PALEG=RGBColor(232,247,239)

def box(s,x,y,w,h,fill,rad=True,line=None):
    sh=s.shapes.add_shape(MSO_SHAPE.ROUNDED_RECTANGLE if rad else MSO_SHAPE.RECTANGLE,
                          Inches(x), Inches(y), Inches(w), Inches(h))
    sh.fill.solid(); sh.fill.fore_color.rgb=fill
    sh.line.color.rgb=line or fill
    return sh

def txt(s,t,x,y,w,h,sz=18,c=TEXT,b=False,align=PP_ALIGN.LEFT):
    tb=s.shapes.add_textbox(Inches(x), Inches(y), Inches(w), Inches(h))
    tf=tb.text_frame; tf.clear(); tf.word_wrap=True; tf.vertical_anchor=MSO_ANCHOR.MIDDLE
    p=tf.paragraphs[0]; p.text=t; p.alignment=align
    r=p.runs[0]; r.font.name="Aptos"; r.font.size=Pt(sz); r.font.bold=b; r.font.color.rgb=c
    return tb

def header(s,kicker,heading,n):
    txt(s,kicker.upper(),.7,.35,4,.25,10,BLUE,True)
    txt(s,heading,.7,.72,11.8,.65,28,NAVY,True)
    txt(s,f"{n:02d}",12.1,7.08,.5,.25,9,MUTED,True,PP_ALIGN.RIGHT)

def footer(s):
    txt(s,"IBM Bob • Asset Inventory Reconciliation",.7,7.08,6,.25,8.5,MUTED)

def pill(s,t,x,y,w,c):
    box(s,x,y,w,.38,c,True,c); txt(s,t,x,y,w,.38,9,WHITE,True,PP_ALIGN.CENTER)

# 1
s=prs.slides.add_slide(prs.slide_layouts[6]); box(s,0,0,13.333,7.5,NAVY,False)
box(s,9.0,0,4.333,7.5,BLUE,False)
for x,y,w,h,c in [(9.45,.9,2.8,.5,CYAN),(9.45,1.7,1.8,.5,WHITE),(11.5,1.7,1.3,.5,PALE),
                  (9.45,2.5,3.35,.5,WHITE),(9.45,3.3,2.4,.5,PALE),(12.1,3.3,.7,.5,WHITE),
                  (9.45,4.1,3.35,.5,CYAN),(9.45,4.9,1.5,.5,WHITE),(11.2,4.9,1.6,.5,PALE)]:
    box(s,x,y,w,h,c,True,c)
txt(s,"IBM BOB",.8,.75,2,.3,12,CYAN,True)
txt(s,"FROM MANUAL\nTO INTELLIGENT",.8,1.55,7.7,1.6,34,WHITE,True)
txt(s,"Asset Inventory Reconciliation",.82,3.35,7.3,.5,22,WHITE,True)
txt(s,"Using AI-assisted development to automate the initial comparison process.",.82,4.1,7.1,.75,16,RGBColor(205,220,235))
txt(s,"Dineo Motloutsi  |  2026",.82,5.5,4,.35,11,RGBColor(180,200,220))
txt(s,"IDEA  →  BUILD  →  AUTOMATE",.82,6.25,5.8,.35,11,CYAN,True)

# 2
s=prs.slides.add_slide(prs.slide_layouts[6]); box(s,0,0,13.333,7.5,BG,False); header(s,"01 — The problem","Two inventories. One reconciliation headache.",2)
txt(s,"The weekly process starts with data from different sources and ends with manual investigation.",.72,1.48,11.7,.45,16,MUTED)
box(s,.75,2.35,3.35,1.55,WHITE,True,RGBColor(225,230,236)); pill(s,"SOURCE",1.05,2.62,1.05,BLUE)
txt(s,"Servers • laptops • desktops",1.05,3.05,2.7,.35,15,NAVY,True); txt(s,"CSV / Excel",1.05,3.4,2.2,.25,11,MUTED)
box(s,4.98,2.35,3.35,1.55,WHITE,True,RGBColor(225,230,236)); pill(s,"FLEXERA",5.28,2.62,1.1,GREEN)
txt(s,"Current inventory export",5.28,3.05,2.7,.35,15,NAVY,True); txt(s,"Asset / agent information",5.28,3.4,2.4,.25,11,MUTED)
txt(s,"+",8.65,2.78,.45,.55,28,BLUE,True,PP_ALIGN.CENTER)
box(s,9.35,2.35,3.15,1.55,NAVY,True,NAVY); txt(s,"MANUAL\nCOMPARISON",9.65,2.62,2.55,.8,18,WHITE,True,PP_ALIGN.CENTER)
txt(s,"The bottleneck",.75,4.65,3,.35,16,NAVY,True)
for i,t in enumerate(["Excel / VLOOKUP","Repetitive matching","Manual discrepancy review"]):
    box(s,.78,5.15+i*.5,.18,.18,BLUE,True,BLUE); txt(s,t,1.12,5.02+i*.5,4.5,.4,13,TEXT)
box(s,7.15,4.75,5.35,1.65,PALE,True,PALE); txt(s,"THE OPPORTUNITY",7.5,5.02,2.4,.28,11,BLUE,True)
txt(s,"Automate the initial comparison — and let people focus on the exceptions.",7.5,5.42,4.45,.65,16,NAVY,True)

# 3
s=prs.slides.add_slide(prs.slide_layouts[6]); box(s,0,0,13.333,7.5,WHITE,False); header(s,"02 — Before automation","The old workflow",3)
steps=[("01","Request","asset list"),("02","Receive","CSV / Excel"),("03","Export","Flexera"),("04","Compare","VLOOKUP"),("05","Identify","differences"),("06","Investigate","agents")]
for i,(n,a,b) in enumerate(steps):
    x=.75+(i%3)*4.15; y=1.85+(i//3)*2.15
    box(s,x,y,3.55,1.55,BG,True,RGBColor(228,233,239))
    box(s,x+.25,y+.25,.55,.55,BLUE,True,BLUE); txt(s,n,x+.25,y+.25,.55,.55,10,WHITE,True,PP_ALIGN.CENTER)
    txt(s,a,x+1.0,y+.23,2.2,.35,15,NAVY,True); txt(s,b,x+1.0,y+.7,2.2,.3,12,MUTED)
txt(s,"TIME IS SPENT FINDING THE DIFFERENCES — NOT RESOLVING THEM.",1.15,6.45,11,.4,16,NAVY,True,PP_ALIGN.CENTER)
footer(s)

# 4
s=prs.slides.add_slide(prs.slide_layouts[6]); box(s,0,0,13.333,7.5,BG,False); header(s,"03 — The idea","What if the comparison happened automatically?",4)
box(s,.75,1.75,5.55,4.65,WHITE,True,RGBColor(225,230,236))
box(s,7.03,1.75,5.55,4.65,NAVY,True,NAVY)
txt(s,"BEFORE",1.1,2.05,1.5,.3,11,MUTED,True)
txt(s,"2 files + Excel + VLOOKUP",1.1,2.55,4.5,.5,21,NAVY,True)
for i,t in enumerate(["Open datasets","Match records","Find differences","Review results"]):
    txt(s,t,1.1,3.35+i*.52,3.8,.3,13,TEXT)
txt(s,"AFTER",7.38,2.05,1.5,.3,11,CYAN,True)
txt(s,"Upload → Compare → Flag → Review",7.38,2.55,4.55,.65,21,WHITE,True)
for i,(t,c) in enumerate([("UPLOAD",CYAN),("COMPARE",BLUE),("FLAG",ORANGE),("REVIEW",GREEN)]):
    box(s,7.38+i*1.15,3.65,.92,.5,c,True,c); txt(s,t,7.38+i*1.15,3.65,.92,.5,8,WHITE,True,PP_ALIGN.CENTER)
txt(s,"The shift: from comparing everything → to investigating what matters.",1.55,6.65,10.2,.35,15,NAVY,True,PP_ALIGN.CENTER)

# 5
s=prs.slides.add_slide(prs.slide_layouts[6]); box(s,0,0,13.333,7.5,WHITE,False); header(s,"04 — IBM Bob","From requirement to working prototype",5)
txt(s,"IBM Bob acts as an AI-powered development partner.",.75,1.45,7,.4,18,NAVY,True)
flow=[("IDEA",BLUE),("PROMPT",CYAN),("BOB",BLUE),("CODE",ORANGE),("APP",GREEN)]
for i,(lab,c) in enumerate(flow):
    x=.8+i*2.45
    box(s,x,2.35,1.8,1.15,c,True,c); txt(s,lab,x,2.35,1.8,1.15,15,WHITE,True,PP_ALIGN.CENTER)
    if i<4: txt(s,"→",x+1.88,2.72,.35,.35,18,MUTED,True,PP_ALIGN.CENTER)
box(s,.85,4.45,11.55,1.25,PALE,True,PALE)
txt(s,"Example development instruction",1.2,4.68,3.2,.3,11,BLUE,True)
txt(s,"“Build a solution that compares two asset inventory files and identifies unmatched assets.”",1.2,5.05,10.5,.45,15,NAVY,True)
txt(s,"Human-in-the-loop: review • test • refine",3.7,6.2,5.9,.35,13,MUTED,True,PP_ALIGN.CENTER)
footer(s)

# 6
s=prs.slides.add_slide(prs.slide_layouts[6]); box(s,0,0,13.333,7.5,BG,False); header(s,"05 — The solution","A simple reconciliation engine",6)
# pipeline
box(s,.7,1.85,2.45,1.35,WHITE,True,RGBColor(225,230,236)); pill(s,"INPUT 01",.95,2.08,.9,BLUE); txt(s,"Source inventory",.95,2.55,1.9,.35,15,NAVY,True)
box(s,.7,3.65,2.45,1.35,WHITE,True,RGBColor(225,230,236)); pill(s,"INPUT 02",.95,3.88,.9,GREEN); txt(s,"Flexera export",.95,4.35,1.9,.35,15,NAVY,True)
txt(s,"→",3.35,3.1,.55,.55,28,BLUE,True,PP_ALIGN.CENTER)
box(s,4.0,2.5,3.05,2.0,NAVY,True,NAVY)
txt(s,"RECONCILIATION\nENGINE",4.35,2.85,2.35,.8,21,WHITE,True,PP_ALIGN.CENTER)
txt(s,"match • compare • classify",4.35,3.75,2.35,.3,10,RGBColor(190,210,230),False,PP_ALIGN.CENTER)
txt(s,"→",7.35,3.1,.55,.55,28,BLUE,True,PP_ALIGN.CENTER)
for i,(h,c,b) in enumerate([("MATCHED",GREEN,"Both sources"),("MISSING",RED,"Source only"),("EXTRA",ORANGE,"Flexera only")]):
    x=8.05+i*1.55
    box(s,x,2.25,1.35,2.45,WHITE,True,RGBColor(225,230,236))
    box(s,x+.22,2.5,.9,.9,c,True,c); txt(s,"✓" if i==0 else "!",x+.22,2.5,.9,.9,22,WHITE,True,PP_ALIGN.CENTER)
    txt(s,h,x+.1,3.65,1.15,.35,10,NAVY,True,PP_ALIGN.CENTER)
    txt(s,b,x+.1,4.1,1.15,.5,9,MUTED,False,PP_ALIGN.CENTER)
box(s,2.1,5.75,9.1,.65,PALEG,True,PALEG); txt(s,"Human review remains focused on the exceptions.",2.3,5.85,8.7,.35,13,GREEN,True,PP_ALIGN.CENTER)
footer(s)

# 7 actual screenshot
s=prs.slides.add_slide(prs.slide_layouts[6]); box(s,0,0,13.333,7.5,WHITE,False); header(s,"06 — Prototype","From concept → working application",7)
img=asset_dir/"Asset reconciliation status table.png"
if img.exists(): s.shapes.add_picture(str(img), Inches(.75), Inches(1.45), width=Inches(11.85), height=Inches(5.3))
else: box(s,.75,1.5,11.85,5.25,BG,True); txt(s,"Application results screenshot",1.2,3.8,11,.5,22,MUTED,True,PP_ALIGN.CENTER)
txt(s,"Sample output: the application surfaces records that require attention.",1.1,6.82,11,.3,11,MUTED,False,PP_ALIGN.CENTER)
footer(s)

# 8 before after visual
s=prs.slides.add_slide(prs.slide_layouts[6]); box(s,0,0,13.333,7.5,BG,False); header(s,"07 — Transformation","The process changes — the objective stays the same",8)
img=asset_dir/"IT asset reconciliation process transformation.png"
if img.exists(): s.shapes.add_picture(str(img), Inches(.65), Inches(1.35), width=Inches(12.0), height=Inches(5.8))
else:
    box(s,.8,1.7,5.55,4.7,WHITE,True); box(s,7,1.7,5.55,4.7,NAVY,True)
footer(s)

# 9
s=prs.slides.add_slide(prs.slide_layouts[6]); box(s,0,0,13.333,7.5,WHITE,False); header(s,"08 — Value","Less comparison. More investigation.",9)
cards=[("LESS","Manual effort",BLUE),("FASTER","Discrepancy identification",GREEN),("CLEARER","Exception visibility",CYAN),("REPEATABLE","Weekly process",ORANGE)]
for i,(a,b,c) in enumerate(cards):
    x=.75+(i%2)*6.1; y=1.65+(i//2)*2.15
    box(s,x,y,5.55,1.7,BG,True,RGBColor(225,230,236))
    txt(s,a,x+.35,y+.28,1.7,.35,13,c,True)
    txt(s,b,x+.35,y+.82,4.7,.42,18,NAVY,True)
box(s,.75,6.1,11.65,.7,NAVY,True,NAVY)
txt(s,"COMPARE LESS  →  INVESTIGATE SMARTER",1,6.2,11.15,.45,17,WHITE,True,PP_ALIGN.CENTER)
footer(s)

# 10
s=prs.slides.add_slide(prs.slide_layouts[6]); box(s,0,0,13.333,7.5,NAVY,False)
txt(s,"09 — NEXT",.8,.65,2,.3,11,CYAN,True); txt(s,"From prototype to scalable automation",.8,1.1,10.8,.6,28,WHITE,True)
future=[("01","Flexera integration","Connect directly to inventory data"),
        ("02","Scheduled runs","Automate the weekly reconciliation"),
        ("03","Alerts","Notify teams about discrepancies"),
        ("04","History","Track recurring exceptions"),
        ("05","Remediation","Move from detection to resolution")]
for i,(n,h,b) in enumerate(future):
    y=2.0+i*.78
    box(s,.85,y,.42,.42,BLUE,True,BLUE); txt(s,n,.85,y,.42,.42,8,WHITE,True,PP_ALIGN.CENTER)
    txt(s,h,1.55,y-.03,3.2,.35,14,WHITE,True); txt(s,b,4.85,y-.03,6.9,.35,12,RGBColor(190,205,220))
box(s,.85,6.2,11.6,.72,BLUE,True,BLUE)
txt(s,"IBM Bob helped turn a manual process bottleneck into a working automation concept.",1.1,6.33,11.1,.42,14,WHITE,True,PP_ALIGN.CENTER)
txt(s,"THANK YOU",10.8,7.08,1.7,.22,9,RGBColor(190,205,220),True,PP_ALIGN.RIGHT)

prs.save(out)
print(out)
