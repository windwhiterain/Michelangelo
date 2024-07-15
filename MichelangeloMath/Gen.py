from pathlib import Path; 
from dataclasses import dataclass

native_ops=["+","-","*","/",">",">=","<","<=","&&","||"]

@dataclass()
class Op2:
    type_a:str
    type_b:str
    op:str
    type_res:str
    def nd(self,_:int)->'Op2':
        return Op2(f"{self.type_a}{_}",f"{self.type_b}{_}",self.op,f"{self.type_res}{_}")
@dataclass()
class Cvs2:
    type_a:str
    type_b:str
    type_res:str

@dataclass()
class Ctc:
    type:str
    op:str
    name:str

def Data1D(comp_type:str,count:int,ops:list[Op2],ctcs:list[Ctc])->str:
    type=f"{comp_type}{count}"
    ids=["x","y","z","w"][0:count]
    id_parameters=[f"{comp_type} {id}" for id in ["x","y","z","w"][0:count]]
    def Op2Each(op:Op2) -> str:
        op_nd=op.nd(count)
        native=op.op in native_ops
        if native:
            return f"""
public static {op_nd.type_res} operator {op_nd.op}({op_nd.type_a} a, {op_nd.type_b} b)=>
    new({",".join([f"a.{id} {op_nd.op} b.{id}" for id in ids])});

public static {op_nd.type_res} operator {op_nd.op}({op_nd.type_a} a, {op.type_b} b)=>
    new({",".join([f"a.{id} {op_nd.op} b" for id in ids])});

public static {op_nd.type_res} operator {op_nd.op}({op.type_b} b, {op_nd.type_a} a)=>
    new({",".join([f"a.{id} {op_nd.op} b" for id in ids])});
"""
        else:
            return f"""
public {op_nd.type_res} {op_nd.op}({op_nd.type_b} b)=>
    new({",".join([f"System.Math.{op_nd.op}({id},b.{id})" for id in ids])});

public {op_nd.type_res} {op_nd.op}({op.type_b} b)=>
    new({",".join([f"System.Math.{op_nd.op}({id},b)" for id in ids])});
"""
    def contraction(ctc:Ctc):
        native=ctc.op in native_ops
        if native:
            return f"""
public {comp_type} {ctc.name}=>
    {ctc.op.join(ids)};
"""
        else:
            return f"""
public {comp_type} {ctc.name}=>
    {"".join([f"{ctc.name}({id}," for id in ids[0:count-1]])}{ids[-1]}{sum([")"]*(count-1))};
"""
    
    return f"""
public record struct {type}
(
    {",\n".join(id_parameters)}
){{
{
    "".join([Op2Each(op) for op in filter(lambda a:a.type_a==comp_type,ops)])+
    "".join([contraction(ctc) for ctc in filter(lambda a:a.type==comp_type,ctcs)])
}
    public {comp_type} this[int _]{{
        get{{
            return _ switch
            {{
                {"".join([f"{i}=>{ids[i]},\n" for i in range(count)])}
                _ => throw new Exception()
            }};
        }}
        set
        {{
            switch (_)
            {{
                {"".join([f"case {i}:{ids[i]}=value;break;\n" for i in range(count)])}
                default:
                    throw new Exception();
            }}
        }}
    }}
    public static IEnumerable<int> AllAxis => Enumerable.Range(0, {count});
    public static IEnumerable<int> OtherAxis(int axis) => AllAxis.Where(a => a != axis);
    public static {type} Diagonal({comp_type} _)
    {{
        return new({",".join(["_"]*count)});
    }}
    public static {type} Basis(int axis,{comp_type} value)
    {{
        var ret = new {type}();
        ret[axis] = value;
        return ret;
    }}
}}
"""

def nd_ops(ops:list[Op2],ND:list[int])->list[Op2]:
    ret=[]
    for op in ops:
        for d in ND:
            ret.append(Op2(f"{op.type_a}{d}",f"{op.type_b}{d}",op.op,f"{op.type_res}{d}"))
    return ret

def cvs_cross_op(cvses:list[Cvs2],ops:list[str])->list[Op2]:
    ret=[]
    for cvs in cvses:
        for op in ops:
            ret.append(Op2(cvs.type_a,cvs.type_b,op,cvs.type_res))
    return ret

def group_cvs(types:list[str],reses:list[str])->list[Cvs2]:
    ret=[]
    for a in types:
        for b in types:
            for res in reses:
                ret.append(Cvs2(a,b,res))
    return ret

type_precise={
    "Float":2,
    "Int":1,
}
def compute_cvs(types:list[str])->list[Cvs2]:
    ret=[]
    for a in types:
        for b in types:
            ret.append(Cvs2(a,b,a if type_precise[a]>type_precise[b] else b))
    return ret

keep_precies=compute_cvs(["Float","Int"])
compare=group_cvs(["Float","Int"],["Bool"])
increase_precies=group_cvs(["Float","Int"],["Float"])

ops=(
    cvs_cross_op(keep_precies,["+","-","*","Max","Min"])+
    cvs_cross_op(compare,[">","<",">=","<="])+
    cvs_cross_op(increase_precies,["/"])
)

ctcs=[
    Ctc("Bool","&&","All"),
    Ctc("Bool","||","Any"),
]
def data1d(comp_types:list[str],counts:list[int])->str:
    ret=""
    for comp_type in comp_types:
        for count in counts:
            ret+=Data1D(comp_type,count,ops,ctcs)
    return ret

with open(Path(__file__).parent/"Gen.cs","w") as f:
    f.write(f"""
namespace Michelangelo.Math;
using Float=float;
using Int=int;
using Bool=bool;
{
    data1d(["Float","Int","Bool"],[2,3,4])
}
""")